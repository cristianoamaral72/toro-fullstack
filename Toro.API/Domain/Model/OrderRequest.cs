namespace Toro.Domain.Model;

public class OrderRequest
{
    public OrderRequest(int productId, int quantity, DateTime dataCadastro)
    {
        Id = productId;
        Quantity = quantity;
        DataCadastro = dataCadastro;
    }

    public int Id { get; set; }
    public int Quantity { get; set; }
    public DateTime DataCadastro { get; set; }
}