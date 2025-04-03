using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Toro.Data.DbContext;
using Toro.Domain.Entities;
using Toro.Domain.Interfaces.Repository;

namespace Toro.Data.Repository;

public class ProductRepository : IProductRepository
{
    private readonly ToroContext _context;

    public ProductRepository(ToroContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllOrderedByTaxAsync()
    {
        return await _context.Products
            .AsNoTracking()
            .OrderByDescending(p => p.Tax)
            .ToListAsync();
    }
    public async Task UpdateAsync(Product product)
    {
        _context.Entry(product).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task AddAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int productId)
    {
        var product = await _context.Products.FindAsync(productId);
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        var transaction = await _context.Database.BeginTransactionAsync();
        return transaction;
    }

    public async Task<Product?> GetByIdAsync(int? productId)
    {
        return await _context.Products.FindAsync(productId);
    }
}