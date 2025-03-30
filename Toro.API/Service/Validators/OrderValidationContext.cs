namespace Toro.Service.Validators;

public class OrderValidationContext
{
    public int ProductStock { get; }
    public decimal AccountBalance { get; }
    public int Quantity { get; }
    public decimal TotalAmount { get; }

    public OrderValidationContext(int productStock, decimal accountBalance, int quantity, decimal totalAmount)
    {
        ProductStock = productStock;
        AccountBalance = accountBalance;
        Quantity = quantity;
        TotalAmount = totalAmount;
    }
}