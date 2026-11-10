using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TrainingManagementSystem.Models;

namespace TrainingManagementSystem.ViewModel
{
    public class CourseEnrollmentFormViewModel
    {
        public int Id { get; set; }
        [Required]
        public DateTime EnrollmentDate { get; set; }
        [Required]
        [EnumDataType(typeof(EnrollmentStatus), ErrorMessage = "Please select a valid status")]
        public EnrollmentStatus Status { get; set; }

        [ForeignKey("Trainee")]
        [Required]
        public int TraineeId { get; set; }
        public SelectList? Trainee { get; set; }
        [ForeignKey("Course")]
        [Required]
        public int CourseId { get; set; }
        public SelectList? Course { get; set; }
    }
}
