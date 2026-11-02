using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using TrainingManagementSystem.Models;

namespace TrainingManagementSystem.ViewModel
{
    public class CourseEnrollmentFormViewModel
    {
        public int Id { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public EnrollmentStatus Status { get; set; }

        [ForeignKey("Trainee")]
        public int TraineeId { get; set; }
        public SelectList? Trainee { get; set; }
        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public SelectList? Course { get; set; }
    }
}
