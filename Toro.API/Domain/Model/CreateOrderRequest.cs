namespace Toro.Domain.Model;

public class CreateOrderRequest
{
    public int? ProductId { get; set; }
    public string? ClientId { get; set; }
    public int Quantity { get; set; }
}