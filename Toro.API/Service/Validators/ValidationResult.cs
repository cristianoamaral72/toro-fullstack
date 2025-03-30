namespace Toro.Service.Validators;

public class ValidationResult
{
    public bool IsValid { get; }
    public IEnumerable<string> Errors { get; }

    public ValidationResult(bool isValid, IEnumerable<string> errors)
    {
        IsValid = isValid;
        Errors = errors;
    }
}