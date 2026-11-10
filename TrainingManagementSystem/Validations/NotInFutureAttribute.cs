using System.ComponentModel.DataAnnotations;

namespace TrainingManagementSystem.Validations
{
    public class NotInFutureAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;
            DateTime dateTime = (DateTime)value;
            if (dateTime.Date > DateTime.Today)
                return new ValidationResult(ErrorMessage ?? "Date cannot be in the future");

            return ValidationResult.Success;
        }
    }
}
