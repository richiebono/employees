using Bono.Employees.Domain.Validations;

namespace Bono.Employees.Domain.Interfaces.Validations
{
    public interface ISelfValidation
    {
        ValidationResult ValidationResult { get; }
        bool IsValid { get; }
    }
}
