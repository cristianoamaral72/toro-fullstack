using Toro.Domain.Base;
using Toro.Domain.Exceptions;

namespace Toro.Domain.Entities;

public class Account
{
    public int Id { get; set; }
    public string ClientId { get; set; }
    public int AccountNumber { get; set; }
    public decimal Balance { get; set; }

    public void DebitBalance(decimal amount)
    {
        if (amount > Balance)
            throw new InvalidOperationException("Saldo insuficiente");
        Balance -= amount;
    }
}