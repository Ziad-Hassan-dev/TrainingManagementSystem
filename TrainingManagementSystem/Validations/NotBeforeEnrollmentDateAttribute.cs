using System.ComponentModel.DataAnnotations;
using TrainingManagementSystem.Models;
using TrainingManagementSystem.ViewModel;

namespace TrainingManagementSystem.Validations
{
    public class NotBeforeEnrollmentDateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;
            DateTime dateTime = (DateTime)value;
            var vm = (CourseResultFormViewModel)validationContext.ObjectInstance;
            using var context = new Context();

            DateTime enrollmentDate =
                context.CourseEnrollments
                .Where(e => e.Id == vm.EnrollmentId)
                .Select(e => e.EnrollmentDate)
                .FirstOrDefault();

            if (dateTime < enrollmentDate)
                return new ValidationResult(ErrorMessage ?? "Result date cannot be before the enrollment date");
            return ValidationResult.Success;
        }
    }
}
