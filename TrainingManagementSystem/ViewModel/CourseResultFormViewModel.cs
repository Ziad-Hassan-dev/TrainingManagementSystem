using Microsoft.AspNetCore.Mvc.Rendering;

namespace TrainingManagementSystem.ViewModel
{
    public class CourseResultFormViewModel
    {
        public int EnrollmentId { get; set; }
        public SelectList? Enrollments { get; set; }
        public int Degree { get; set; }
        public DateTime ResultDate { get; set; }
    }
}
