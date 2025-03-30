namespace Toro.Domain.Model;

public class OrderResult
{
    public bool IsSuccess { get; private set; }
    public OrderResponse Response { get; private set; }
    public IEnumerable<string> Errors { get; private set; }

    public static OrderResult Success(OrderResponse response) =>
        new OrderResult { IsSuccess = true, Response = response };
    public static OrderResult Failure(IEnumerable<string> errors) =>
        new OrderResult { IsSuccess = false, Errors = errors };
}
