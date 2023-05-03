using System;
using System.ComponentModel.DataAnnotations;

namespace Assignment_WebBank.Infrastructure.Validation
{
    public class MinValueAttribute : ValidationAttribute
    {
        private readonly int _minValue;

        public MinValueAttribute(int minValue)
        {
            _minValue = minValue;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null && (int)value < _minValue)
            {
                return new ValidationResult($"The to Account field must be greater than 0.");
            }

            return ValidationResult.Success;
        }
    }

    public class NotEqualToAttribute : ValidationAttribute
    {
        private readonly string _comparisonValue;

        public NotEqualToAttribute(string comparisonValue)
        {
            _comparisonValue = comparisonValue;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || value.ToString() == _comparisonValue)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}

