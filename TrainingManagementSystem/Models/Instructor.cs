using System.ComponentModel.DataAnnotations.Schema;

namespace TrainingManagementSystem.Models
{
    public class Instructor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string ImageURL { get; set; }
        [ForeignKey("Department")]
        public int DeptId { get; set; } 
        public Department Department { get; set; } = null!;
        public ICollection<Course> Courses { get; set; }
    }
}
