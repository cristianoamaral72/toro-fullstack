using Toro.Domain.Base;
using Toro.Domain.Exceptions;

namespace Toro.Domain.Entities;

public class Product
{
    public int ProductId { get; set; } // Identificador único do produto
    public string BondAsset { get; set; } = string.Empty; // Tipo do produto (ex.: CDB, LCI)
    public string Index { get; set; } = string.Empty; // Indexador (ex.: IPCA, Selic)
    public decimal Tax { get; set; } // Taxa atrelada ao indexador
    public string IssuerName { get; set; } = string.Empty; // Nome do emissor do produto
    public decimal UnitPrice { get; set; } // Preço unitário do produto
    public int Stock { get; set; } // Estoque disponível do produto
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow; // Data de criação
    public DateTime ModifiedDate { get; set; } = DateTime.UtcNow; // Data de modificação

    // Métodos de domínio
    public void DebitStock(int quantity)
    {
        if (quantity > Stock)
            throw new InvalidOperationException("Estoque insuficiente para a operação.");
        Stock -= quantity;
    }
}