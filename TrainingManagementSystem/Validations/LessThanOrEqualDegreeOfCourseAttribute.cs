using System.ComponentModel.DataAnnotations;
using TrainingManagementSystem.Models;

namespace TrainingManagementSystem.Validations
{
    public class LessThanOrEqualDegreeOfCourseAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;
            int degree = (int)value;
            using var context = new Context();
            var vm = (CourseResult)validationContext.ObjectInstance;

            int? courseDegree =
                context.CourseEnrollments
                .Where(e => e.Id == vm.EnrollmentId)
                .Select(e => e.Course.Degree)
                .FirstOrDefault();

            if(degree<=courseDegree)
                return ValidationResult.Success;

            return new ValidationResult(ErrorMessage ?? $"Degree must not exceed the course degree ({courseDegree})");
        }
    }
}
