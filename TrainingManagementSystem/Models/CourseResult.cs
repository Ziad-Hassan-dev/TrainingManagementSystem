using System.ComponentModel.DataAnnotations.Schema;

namespace TrainingManagementSystem.Models
{
    public class CourseResult
    {
        public int EnrollmentId { get; set; }
        public int Degree { get; set; }
        public DateTime ResultDate { get; set; }
        public CourseEnrollment CourseEnrollment { get; set; }
    }
}
