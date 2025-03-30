using Microsoft.EntityFrameworkCore.Storage;
using Toro.Domain.Entities;

namespace Toro.Domain.Interfaces.Repository;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllOrderedByTaxAsync();
    Task<Product?> GetByIdAsync(int? productId);
    Task UpdateAsync(Product product);
    Task AddAsync(Product product);
    Task DeleteAsync(int productId);
    Task<IDbContextTransaction> BeginTransactionAsync();
}