using Toro.Domain.Entities;

namespace Toro.Domain.Interfaces.Repository;

public interface IAccountRepository
{
    Task<Account?> GetByClientIdAsync(string clientId);
    Task UpdateAsync(Account account);
    Task AddAsync(Account account);
    Task<Account?> GetByIdAsync(int accountId);
    Task<IEnumerable<Account>> GetAllAsync();
    Task DeleteAsync(Account account);
}