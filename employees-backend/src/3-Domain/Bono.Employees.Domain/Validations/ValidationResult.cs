using System;
using System.Collections.Generic;
using System.Linq;

namespace Bono.Employees.Domain.Validations
{
    public class ValidationResult
    {
        public dynamic Data { get; set; }
        private readonly List<ValidationError> _Errors;
        private string Message { get; set; }
        public bool IsValid { get { return !_Errors.Any(); } }
        public IEnumerable<ValidationError> Errors { get { return _Errors; } }
        
        public ValidationResult()
        {
            _Errors = new List<ValidationError>();
            Message = string.Empty;
        }        

        public ValidationResult Add(string errorMessage)
        {
            _Errors.Add(new ValidationError(errorMessage));
            return this;
        }

        public ValidationResult Add(ValidationError error)
        {
            _Errors.Add(error);
            return this;
        }

        public ValidationResult Add(params ValidationResult[] validationResults)
        {
            if (validationResults == null) return this;

            foreach (var result in validationResults.Where(r => r != null))
                _Errors.AddRange(result.Errors);

            return this;
        }

        public ValidationResult Remove(ValidationError error)
        {
            if (_Errors.Contains(error))
                _Errors.Remove(error);
            return this;
        }
    }
}
