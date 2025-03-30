using System.ComponentModel.DataAnnotations;
using System.Threading.Channels;
using Toro.Domain.Entities;
using Toro.Domain.Interfaces.Repository;
using Toro.Domain.Interfaces.Service;
using Toro.Domain.Model;
using Toro.Service.Validators;
using ValidationResult = Toro.Service.Validators.ValidationResult;

namespace Toro.Service.Services;

public class OrderProcessingService : IOrderProcessingService
{
    private readonly IProductRepository _productRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly Channel<OrderRequest> _orderChannel;

    public OrderProcessingService(
        IProductRepository productRepository,
        IAccountRepository accountRepository)
    {
        _productRepository = productRepository;
        _accountRepository = accountRepository;
        _orderChannel = Channel.CreateBounded<OrderRequest>(100);
    }

    public async Task<OrderResult> ProcessOrderAsync(CreateOrderRequest request)
    {
        // Busca o produto e a conta de forma concorrente para otimizar a performance.
        var productTask = _productRepository.GetByIdAsync(request.ProductId);
        var accountTask = _accountRepository.GetByClientIdAsync(request.ClientId);
        await Task.WhenAll(productTask, accountTask);

        var product = productTask.Result;
        var account = accountTask.Result;

        // Calcula o valor total e valida o pedido.
        var totalAmount = product.UnitPrice * request.Quantity;
        var validationResult = ValidateOrder(product, account, request.Quantity, totalAmount);
        if (!validationResult.IsValid)
            return OrderResult.Failure(validationResult.Errors);

        // Inicia uma transação.
        await using var transaction = await _productRepository.BeginTransactionAsync();

        try
        {
            // Realiza as operações de negócio.
            product.DecreaseStock(request.Quantity);
            if (account != null)
            {
                account.DebitBalance(totalAmount);

                // Atualiza as entidades de forma concorrente.
                var updateProductTask = _productRepository.UpdateAsync(product);
                var updateAccountTask = _accountRepository.UpdateAsync(account);
                await Task.WhenAll(updateProductTask, updateAccountTask);

                // Envia a solicitação de pedido para o canal.
                await _orderChannel.Writer.WriteAsync(new OrderRequest(
                    product.Id,
                    request.Quantity,
                    DateTime.UtcNow));

                // Confirma a transação.
                await transaction.CommitAsync();

                return OrderResult.Success(new OrderResponse(
                    product.Id,
                    request.Quantity,
                    totalAmount,
                    account.Balance));
            }
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }

        return default;
    }

    private ValidationResult ValidateOrder(Product product, Account account, int quantity, decimal totalAmount)
    {
        var validator = new OrderValidator();
        var context = new OrderValidationContext(product.Stock, account.Balance, quantity, totalAmount);
        return validator.Validate(context);
    }


}