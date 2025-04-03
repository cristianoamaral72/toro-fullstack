using System;
using System.Threading.Channels;
using System.Threading.Tasks;
using Toro.Domain.Entities;
using Toro.Domain.Interfaces.Repository;
using Toro.Domain.Interfaces.Service;
using Toro.Domain.Model;
using Toro.Service.Validators;
using ValidationResult = Toro.Service.Validators.ValidationResult;

namespace Toro.Service.Services
{
    public class OrderProcessingService : IOrderProcessingService
    {
        private readonly IProductRepository _productRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly Channel<OrderRequest> _orderChannel;
        private readonly IOrderRepository _orderRepository;

        public OrderProcessingService(
            IProductRepository productRepository,
            IAccountRepository accountRepository, IOrderRepository orderRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
            _orderRepository = orderRepository;
            _orderChannel = Channel.CreateBounded<OrderRequest>(100); // Canal com capacidade limitada
        }

        public async Task<OrderResult> ProcessOrderAsync(CreateOrderRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            // Busca o produto e a conta de forma concorrente
            var (product, account) = await FetchProductAndAccountAsync(request);

            // Valida o pedido
            var totalAmount = CalculateTotalAmount(product.UnitPrice, request.Quantity);
            var validationResult = ValidateOrder(product, account, request.Quantity, totalAmount);
            if (!validationResult.IsValid)
                return OrderResult.Failure(validationResult.Errors);

            // Processa a transação e atualiza os dados
            return await ProcessTransactionAsync(product, account, request, totalAmount);
        }

        private async Task<(Product product, Account account)> FetchProductAndAccountAsync(CreateOrderRequest request)
        {
            var product = await _productRepository.GetByIdAsync(request.ProductId);
            var account = await _accountRepository.GetByClientIdAsync(request.ClientId);

            if (product == null)
                throw new InvalidOperationException($"Produto com ID {request.ProductId} não encontrado.");
            if (account == null)
                throw new InvalidOperationException($"Conta com ClientId {request.ClientId} não encontrada.");

            return (product, account);
        }

        private decimal CalculateTotalAmount(decimal unitPrice, int quantity)
        {
            return unitPrice * quantity;
        }

        private ValidationResult ValidateOrder(Product product, Account account, int quantity, decimal totalAmount)
        {
            var validator = new OrderValidator();
            var context = new OrderValidationContext(product.Stock, account.Balance, quantity, totalAmount);
            return validator.Validate(context);
        }

        private async Task<OrderResult> ProcessTransactionAsync(Product product, Account account, CreateOrderRequest request, decimal totalAmount)
        {
            await using var transaction = await _productRepository.BeginTransactionAsync();

            try
            {
                // Atualiza estoque e saldo
                product.DebitStock(request.Quantity);
                account.DebitBalance(totalAmount);

                // Cria uma nova ordem
                var order = new Order
                {
                    AccountId = account.AccountId,
                    ProductId = product.ProductId,
                    Quantity = request.Quantity,
                    TotalPrice = totalAmount,
                    CreatedDate = DateTime.UtcNow
                };

                // Atualiza as entidades no banco de dados de forma concorrente
                await _productRepository.UpdateAsync(product);
                await _accountRepository.UpdateAsync(account);
                await _orderRepository.AddAsync(order); // Salva a nova ordem no banco


                // Envia a solicitação de pedido para o canal
                await _orderChannel.Writer.WriteAsync(new OrderRequest(
                    product.ProductId,
                    request.Quantity,
                    DateTime.UtcNow));

                // Confirma a transação
                await transaction.CommitAsync();

                return OrderResult.Success(new OrderResponse(
                    product.ProductId,
                    request.Quantity,
                    totalAmount,
                    account.Balance));
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new InvalidOperationException("Erro ao processar a transação.", ex);
            }
        }
    }
}