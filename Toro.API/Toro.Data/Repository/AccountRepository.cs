using Microsoft.EntityFrameworkCore;
using Toro.Data.DbContext;
using Toro.Domain.Entities;
using Toro.Domain.Interfaces.Repository;

namespace Toro.Data.Repository;

public class AccountRepository : IAccountRepository
{
    private readonly ApplicationDbContext _context;

    public AccountRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Account?> GetByClientIdAsync(string clientId)
    {
        return await _context.Accounts.FirstOrDefaultAsync(a => a.ClientId == clientId);
    }

    public async Task UpdateAsync(Account account)
    {
        _context.Entry(account).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task AddAsync(Account account)
    {
        _context.Accounts.Add(account);
        await _context.SaveChangesAsync();
    }

    public async Task<Account?> GetByIdAsync(int accountId)
    {
        return await _context.Accounts.FindAsync(accountId);
    }

    public async Task<IEnumerable<Account>> GetAllAsync()
    {
        return await _context.Accounts.ToListAsync();
    }

    public async Task DeleteAsync(Account account)
    {
        Account? accountToDelete = await _context.Accounts.FindAsync(account.Id);
        _context.Accounts.Remove(accountToDelete);
        await _context.SaveChangesAsync();
    }
}