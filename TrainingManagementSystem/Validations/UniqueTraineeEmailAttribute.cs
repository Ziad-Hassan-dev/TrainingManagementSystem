using System.ComponentModel.DataAnnotations;
using TrainingManagementSystem.Models;
using TrainingManagementSystem.ViewModel;

namespace TrainingManagementSystem.Validations
{
    public class UniqueTraineeEmailAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            string email = value.ToString()!.Trim();
            var vm = (TraineeFormViewModel)validationContext.ObjectInstance;
            using var context = new Context();
            bool exists =
                context.Trainees.Any(t => t.Email == email && t.Id != vm.Id);
            if (exists)
                return new ValidationResult("Email Must be Unique");

            return ValidationResult.Success;
        }
    }
}
