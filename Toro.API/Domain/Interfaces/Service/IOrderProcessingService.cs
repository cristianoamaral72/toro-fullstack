using Toro.Domain.Entities;
using Toro.Domain.Model;

namespace Toro.Domain.Interfaces.Service;

public interface IOrderProcessingService
{
    Task<OrderResult> ProcessOrderAsync(CreateOrderRequest request);
}