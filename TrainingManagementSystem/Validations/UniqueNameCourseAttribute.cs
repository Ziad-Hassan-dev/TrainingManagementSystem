using System.ComponentModel.DataAnnotations;
using TrainingManagementSystem.Models;
using TrainingManagementSystem.ViewModel;

namespace TrainingManagementSystem.Validations
{
    public class UniqueNameCourseAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;
            string name = value.ToString()!.Trim();
            var vm = (CourseFormViewModel)validationContext.ObjectInstance;
            
            using var context = new Context();

            bool exists =
                context.Courses.Any(c =>
                c.Name == name &&
                c.DeptId == vm.DeptId &&
                c.Id != vm.Id);

            if (exists)
                return new ValidationResult("A course with this name already exists in this department");

            return ValidationResult.Success;
        }
    }
}
