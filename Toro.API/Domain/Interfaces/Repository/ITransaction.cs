namespace Toro.Domain.Interfaces.Repository;

public interface ITransaction : IAsyncDisposable
{
    Task CommitAsync();
    Task RollbackAsync();
}