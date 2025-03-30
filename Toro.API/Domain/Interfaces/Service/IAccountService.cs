using Toro.Domain.Entities;

namespace Toro.Domain.Interfaces.Service;

public interface IAccountService
{
    Task<Account?> GetByClientIdAsync(string clientId);
    Task UpdateAsync(Account account);
    Task AddAsync(Account account);
    Task<Account?> GetByIdAsync(int accountId);
    Task<IEnumerable<Account>> GetAllAsync();
    Task DeleteAsync(Account account);
}