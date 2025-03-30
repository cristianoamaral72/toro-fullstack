namespace Toro.Domain.Model;

public class OrderResponse
{
    public int ProductId { get; }
    public int Quantity { get; }
    public decimal TotalAmount { get; }
    public decimal AccountBalance { get; }

    public OrderResponse(int productId, int quantity, decimal totalAmount, decimal accountBalance)
    {
        ProductId = productId;
        Quantity = quantity;
        TotalAmount = totalAmount;
        AccountBalance = accountBalance;
    }
}