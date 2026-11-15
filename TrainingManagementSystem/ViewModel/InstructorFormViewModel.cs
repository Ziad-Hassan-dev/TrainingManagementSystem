using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TrainingManagementSystem.Models;
using TrainingManagementSystem.Validations;

namespace TrainingManagementSystem.ViewModel
{
    public class InstructorFormViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name Must between 100 and 2")]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        [UniqueEmailInstructorAttribute]
        [StringLength(100)]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^01[0125]\d{8}$", ErrorMessage = "Enter a valid Egyptian mobile number")]
        public string Phone { get; set; }
        [Required]
        [MaxLength(200)]
        public string Address { get; set; }
        [MaxLength(300)]
        public string? ImageURL { get; set; }
        [ForeignKey("Department")]
        [Required]
        [Display(Name = "Department")]
        public int DeptId { get; set; }
        public SelectList? Departments { get; set; }
    }
}
