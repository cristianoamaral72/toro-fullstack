using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Toro.Data.DbContext;
using Toro.Domain.Entities;
using Toro.Domain.Interfaces.Repository;

namespace Toro.Data.Repository;

public class OrderRepository : IOrderRepository
{
    private readonly ToroContext _context;

    public OrderRepository(ToroContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Order>> GetAllOrderedByTaxAsync()
    {
        return await _context.Orders
            .AsNoTracking()
            .OrderByDescending(p => p.OrderId)
            .ToListAsync();
    }
    public async Task UpdateAsync(Order order)
    {
        _context.Entry(order).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task AddAsync(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int orderId)
    {
        var order = await _context.Orders.FindAsync(orderId);
        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        var transaction = await _context.Database.BeginTransactionAsync();
        return transaction;
    }

    public async Task<Order?> GetByIdAsync(int? orderId)
    {
        return await _context.Orders.FindAsync(orderId);
    }
}