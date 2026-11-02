using System.ComponentModel.DataAnnotations;
using TrainingManagementSystem.Validations;

namespace TrainingManagementSystem.ViewModel
{
    public class DepartmentFormViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Name is required")]
        [StringLength(50, MinimumLength = 2,ErrorMessage ="Length Of Name between 2 and 50")]
        [UniqueName(ErrorMessage ="Name Must be Unique")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Description is required")]
        [StringLength(200,ErrorMessage ="Length Of Description must be less than 200")]
        public string Description { get; set; }
    }
}
