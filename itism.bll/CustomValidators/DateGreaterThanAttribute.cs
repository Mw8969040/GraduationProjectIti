using System;
using System.ComponentModel.DataAnnotations;

namespace itism.bll.CustomValidators
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DateGreaterThanAttribute : ValidationAttribute
    {
        private readonly string otherPropertyName;

        public DateGreaterThanAttribute(string otherPropertyName)
        {
            this.otherPropertyName = otherPropertyName;
            ErrorMessage = "{0} must be after {1}.";
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var otherProperty = validationContext.ObjectType.GetProperty(otherPropertyName);
            if (otherProperty == null)
            {
                return new ValidationResult($"Unknown property: {otherPropertyName}");
            }

            var otherValue = otherProperty.GetValue(validationContext.ObjectInstance);
            if (value == null || otherValue == null)
            {
                return ValidationResult.Success;
            }

            if (value is DateTime current && otherValue is DateTime other)
            {
                if (current <= other)
                {
                    var message = string.Format(ErrorMessageString, validationContext.DisplayName, otherPropertyName);
                    return new ValidationResult(message);
                }
            }

            return ValidationResult.Success;
        }
    }
}


