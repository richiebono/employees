namespace Bono.Employees.Domain.Validations
{
    public class ValidationError
    {
        public string Message { get; set; }
        public ValidationError(string message)
        {
            Message = message;
        }
    }
}
