using System.ComponentModel.DataAnnotations;
using TrainingManagementSystem.Validations;
namespace TrainingManagementSystem.ViewModel
{
    public class TraineeFormViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        [EmailAddress]
        [UniqueTraineeEmail]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^01[0125]\d{8}$",ErrorMessage = "Enter a valid Egyptian mobile number")]
        public string Phone { get; set; }
        [Required]
        [StringLength(200)]
        public string Address { get; set; }
        [StringLength(300)]
        public string? ImageURL { get; set; }
    }
}
