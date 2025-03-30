namespace Toro.Domain.Exceptions;

public class InsufficientStockException : Exception
{
    public decimal Balance { get; }
    public decimal Amount { get; }

    public InsufficientStockException(decimal balance, decimal amount)
        : base($"Insufficient stock. Available: {balance}, Requested: {amount}.")
    {
        Balance = balance;
        Amount = amount;
    }

}

