using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainingManagementSystem.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Degree { get; set; }
        public int MinDegree { get; set; }
        public int Hours { get; set; }
        [ForeignKey(nameof(Department))]
        public int DeptId { get; set; }
        [ForeignKey(nameof(Instructor))]
        public int? InsId { get; set; }
        public Instructor Instructor { get; set; }
        public Department Department { get; set; }
        public ICollection<CourseEnrollment> CourseEnrollments { get; set; }
    }
}
