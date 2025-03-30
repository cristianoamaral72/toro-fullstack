using Microsoft.Extensions.Logging;
using Toro.Domain.Entities;
using Toro.Domain.Interfaces.Repository;
using Toro.Domain.Interfaces.Service;

namespace Toro.Service.Services;

public class AccountService: IAccountService
{
    private readonly ILogger<AccountService> _logger;
    private readonly IAccountRepository _accountRepository;

    public AccountService(ILogger<AccountService> logger)
    {
        _logger = logger;
    }

    public async Task<Account> GetByClientIdAsync(string clientId)
    {
        return await _accountRepository.GetByClientIdAsync(clientId);
    }

    public async Task UpdateAsync(Account account)
    {
        await _accountRepository.UpdateAsync(account);
    }

    public async Task AddAsync(Account account)
    {
        await _accountRepository.AddAsync(account);
    }

    public Task<Account> GetByIdAsync(int accountId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Account>> GetAllAsync()
    {
        return await _accountRepository.GetAllAsync();
    }

    public async Task DeleteAsync(Account account)
    {
        await _accountRepository.DeleteAsync(account);
    }
}