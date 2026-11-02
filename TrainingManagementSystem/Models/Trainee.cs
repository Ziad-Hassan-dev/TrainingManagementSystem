namespace TrainingManagementSystem.Models
{
    public class Trainee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string ImageURL { get; set; }
        public ICollection<CourseEnrollment> CourseEnrollments { get; set; }
    }
}
