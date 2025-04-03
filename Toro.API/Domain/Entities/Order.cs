namespace Toro.Domain.Entities;

public class Order
{
    public int OrderId { get; set; } // Identificador único do pedido
    public int AccountId { get; set; } // Relacionamento com a conta Toro
    public int ProductId { get; set; } // Relacionamento com o produto
    public int Quantity { get; set; } // Quantidade comprada
    public decimal TotalPrice { get; set; } // Preço total da compra
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow; // Data de criação do pedido

    // Relacionamentos
    public Account? Account { get; set; } // Relacionamento com a conta
    public Product? Product { get; set; } // Relacionamento com o produto
}