using Bono.Employees.Domain.Validations;

namespace Bono.Employees.Domain.Interfaces.Validations
{
    public interface IValidation<in TEntity>
    {
        ValidationResult Valid(TEntity entity);
    }
}
