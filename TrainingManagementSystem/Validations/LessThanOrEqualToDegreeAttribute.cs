using System.ComponentModel.DataAnnotations;
using TrainingManagementSystem.Models;
using TrainingManagementSystem.ViewModel;

namespace TrainingManagementSystem.Validations
{
    public class LessThanOrEqualToDegreeAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;
            
            int minDegree = (int)value;
            var vm = (CourseFormViewModel)validationContext.ObjectInstance;
            using var context = new Context();

            if (minDegree <= vm.Degree) return ValidationResult.Success;

            return new ValidationResult(ErrorMessage ?? "Minimum degree must be less than or equal to the course degree");
        }
    }
}
