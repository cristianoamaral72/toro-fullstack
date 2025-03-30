using Toro.Domain.Base;
using Toro.Domain.Exceptions;

namespace Toro.Domain.Entities;

public class Product
{
    public int Id { get; set; }
    public string BondAsset { get; set; }
    public string Index { get; set; }
    public decimal Tax { get; set; }
    public string IssuerName { get; set; }
    public decimal UnitPrice { get; set; }
    public int Stock { get; set; }

    public void DecreaseStock(int quantity)
    {
        if (quantity > Stock)
            throw new InvalidOperationException("Estoque insuficiente");
        Stock -= quantity;
    }
}