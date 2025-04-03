using Toro.Domain.Base;
using Toro.Domain.Exceptions;

namespace Toro.Domain.Entities;

public class Account
{
    public int AccountId { get; set; } // Identificador único da conta
    public string ClientId { get; set; } = string.Empty; // Identificador do cliente
    public decimal Balance { get; set; } // Saldo disponível na conta
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow; // Data de criação
    public DateTime ModifiedDate { get; set; } = DateTime.UtcNow; // Data de modificação

    // Métodos de domínio
    public void DebitBalance(decimal amount)
    {
        if (amount > Balance)
            throw new InvalidOperationException("Saldo insuficiente para a operação.");
        Balance -= amount;
    }

    public void CreditBalance(decimal amount)
    {
        Balance += amount;
    }
}