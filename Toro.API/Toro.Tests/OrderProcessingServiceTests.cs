using Microsoft.EntityFrameworkCore.Storage;
using Moq;
using Toro.Data.Transactions;
using Toro.Domain.Entities;
using Toro.Domain.Interfaces.Repository;
using Toro.Domain.Model;
using Toro.Service.Services;

namespace Toro.Tests
{
    [TestClass]
    public class OrderProcessingServiceTests
    {
        private Mock<IProductRepository> _mockProductRepository;
        private Mock<IAccountRepository> _mockAccountRepository;
        private Mock<IDbContextTransaction> _mockDbContextTransaction;
        private OrderProcessingService _orderProcessingService;
        private readonly int _productId = 1;
        private readonly string _clientId = "1234";

        [TestInitialize]
        public void Setup()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _mockAccountRepository = new Mock<IAccountRepository>();
            _mockDbContextTransaction = new Mock<IDbContextTransaction>();

            _mockDbContextTransaction.Setup(t => t.CommitAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
            _mockDbContextTransaction.Setup(t => t.RollbackAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

            _mockProductRepository.Setup(r => r.BeginTransactionAsync())
                .ReturnsAsync(_mockDbContextTransaction.Object);

            _orderProcessingService = new OrderProcessingService(
                _mockProductRepository.Object,
                _mockAccountRepository.Object);
        }

        private static Product CreateProduct(decimal unitPrice, int stock)
        {
            return new Product { UnitPrice = unitPrice, Stock = stock };
        }

        private static Account CreateAccount(string clientId, decimal balance)
        {
            return new Account { ClientId = clientId, Balance = balance };
        }

        private CreateOrderRequest CreateOrderRequest(int quantity)
        {
            return new CreateOrderRequest
            {
                ProductId = _productId,
                ClientId = _clientId,
                Quantity = quantity
            };
        }

        [TestMethod]
        public async Task ProcessOrderAsync_SuccessfulOrder_ReturnsSuccessResult()
        {
            var product = CreateProduct(50m, 10);
            var account = CreateAccount(_clientId, 200m);

            _mockProductRepository.Setup(r => r.GetByIdAsync(_productId)).ReturnsAsync(product);
            _mockAccountRepository.Setup(r => r.GetByClientIdAsync(_clientId)).ReturnsAsync(account);
            _mockProductRepository.Setup(r => r.UpdateAsync(It.IsAny<Product>())).Returns(Task.CompletedTask);
            _mockAccountRepository.Setup(r => r.UpdateAsync(It.IsAny<Account>())).Returns(Task.CompletedTask);

            var request = CreateOrderRequest(2);

            var result = await _orderProcessingService.ProcessOrderAsync(request);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(request.Quantity, result.Response.Quantity);
            Assert.AreEqual(product.UnitPrice * request.Quantity, result.Response.TotalAmount);
        }

        [TestMethod]
        public async Task ProcessOrderAsync_InsufficientStock_ReturnsFailureResult()
        {
            var product = CreateProduct(50m, 5);
            var account = CreateAccount(_clientId, 200m);

            _mockProductRepository.Setup(r => r.GetByIdAsync(_productId)).ReturnsAsync(product);
            _mockAccountRepository.Setup(r => r.GetByClientIdAsync(_clientId)).ReturnsAsync(account);

            var request = CreateOrderRequest(10);

            var result = await _orderProcessingService.ProcessOrderAsync(request);

            Assert.IsFalse(result.IsSuccess);
            Assert.IsNotNull(result.Errors);
        }

        [TestMethod]
        public async Task ProcessOrderAsync_ExceptionDuringUpdate_RollsBackTransaction()
        {
            var product = CreateProduct(50m, 10);
            var account = CreateAccount(_clientId, 200m);

            _mockProductRepository.Setup(r => r.GetByIdAsync(_productId)).ReturnsAsync(product);
            _mockAccountRepository.Setup(r => r.GetByClientIdAsync(_clientId)).ReturnsAsync(account);
            _mockProductRepository.Setup(r => r.UpdateAsync(It.IsAny<Product>())).ThrowsAsync(new Exception("Erro ao atualizar"));

            var request = CreateOrderRequest(2);

            await Assert.ThrowsExceptionAsync<Exception>(() => _orderProcessingService.ProcessOrderAsync(request));

            _mockDbContextTransaction.Verify(t => t.RollbackAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
