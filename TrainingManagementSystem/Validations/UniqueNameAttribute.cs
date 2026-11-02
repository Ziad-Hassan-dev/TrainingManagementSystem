using System.ComponentModel.DataAnnotations;
using TrainingManagementSystem.Models;
using TrainingManagementSystem.ViewModel;

namespace TrainingManagementSystem.Validations
{
    public class UniqueNameAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;
            string name = value.ToString()!.Trim();

            var vm = (DepartmentFormViewModel)validationContext.ObjectInstance;

            using Context context = new Context();
            bool exists = 
                context.Departments.Any(d => d.Name == name && d.Id != vm.Id);

            if (exists)
                return new ValidationResult("Name Must be Unique");

            return ValidationResult.Success;
        }
    }
}
