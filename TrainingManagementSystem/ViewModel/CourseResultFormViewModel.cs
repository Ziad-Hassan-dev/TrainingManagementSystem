using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using TrainingManagementSystem.Validations;

namespace TrainingManagementSystem.ViewModel
{
    public class CourseResultFormViewModel
    {
        [Required]
        public int EnrollmentId { get; set; }
        public SelectList? Enrollments { get; set; }
        [Required]
        [LessThanOrEqualDegreeOfCourse]
        public int Degree { get; set; }
        [Required]
        [NotBeforeEnrollmentDate]
        [NotInFuture]
        public DateTime ResultDate { get; set; }
    }
}
