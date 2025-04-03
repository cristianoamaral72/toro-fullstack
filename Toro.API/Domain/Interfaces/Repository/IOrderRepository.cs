using Microsoft.EntityFrameworkCore.Storage;
using Toro.Domain.Entities;

namespace Toro.Domain.Interfaces.Repository;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetAllOrderedByTaxAsync();
    Task<Order?> GetByIdAsync(int? orderId);
    Task UpdateAsync(Order order);
    Task AddAsync(Order order);
    Task DeleteAsync(int orderId);
    Task<IDbContextTransaction> BeginTransactionAsync();
}