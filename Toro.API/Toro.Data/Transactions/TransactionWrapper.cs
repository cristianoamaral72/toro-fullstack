using Microsoft.EntityFrameworkCore.Storage;
using Toro.Domain.Interfaces.Repository;

namespace Toro.Data.Transactions;

public class TransactionWrapper : ITransaction
{
    private readonly IDbContextTransaction _dbTransaction;

    public TransactionWrapper(IDbContextTransaction dbTransaction)
    {
        _dbTransaction = dbTransaction ?? throw new ArgumentNullException(nameof(dbTransaction));
    }

    public async Task CommitAsync() => await _dbTransaction.CommitAsync();

    public async Task RollbackAsync() => await _dbTransaction.RollbackAsync();

    public async ValueTask DisposeAsync()
    {
        await _dbTransaction.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}