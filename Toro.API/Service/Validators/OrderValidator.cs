using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Toro.Service.Validators;

public class OrderValidator
{
    public ValidationResult Validate(OrderValidationContext context)
    {
        var errors = new List<string>();

        if (context.Quantity <= 0)
            errors.Add("A quantidade deve ser maior que zero.");

        if (context.ProductStock < context.Quantity)
            errors.Add("Estoque insuficiente.");

        if (context.AccountBalance < context.TotalAmount)
            errors.Add("Saldo insuficiente.");

        bool isValid = errors.Count == 0;
        return new ValidationResult(isValid, errors);
    }
}
