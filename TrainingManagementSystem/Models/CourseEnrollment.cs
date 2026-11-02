using System.ComponentModel.DataAnnotations.Schema;

namespace TrainingManagementSystem.Models
{
    public enum EnrollmentStatus
    {
        Active,
        Completed,
        Cancelled,
    }
    public class CourseEnrollment
    {
        public int Id { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public EnrollmentStatus Status { get; set; }

        [ForeignKey("Trainee")]
        public int TraineeId { get; set; }
        public Trainee Trainee { get; set; }
        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public CourseResult? CourseResult { get; set; }
    }
}
