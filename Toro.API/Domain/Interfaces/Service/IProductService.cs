using Toro.Domain.Entities;

namespace Toro.Domain.Interfaces.Service;

public interface IProductService
{
    Task<IEnumerable<Product?>> GetAllOrderedByTaxAsync();
    Task<Product?> GetByIdAsync(int? productId);
    Task UpdateAsync(Product product);
    Task AddAsync(Product product);
    Task DeleteAsync(int productId);
}