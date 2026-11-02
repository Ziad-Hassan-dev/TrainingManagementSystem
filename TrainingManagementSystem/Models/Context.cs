using Microsoft.EntityFrameworkCore;

namespace TrainingManagementSystem.Models
{
    public class Context : DbContext
    {
        public Context()
        {
            
        }
        public DbSet<Trainee> Trainees { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseResult> CourseResults { get; set; }
        public DbSet<CourseEnrollment> CourseEnrollments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Take Connection String
            optionsBuilder.UseSqlServer("Data Source=ZIAD;Initial Catalog=TMSystem;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CourseResult>().HasKey(cr => cr.EnrollmentId);

            // to define one-to-one relationship between courseEnrollment & courseResult
            modelBuilder.Entity<CourseEnrollment>()
                .HasOne(ce => ce.CourseResult)
                .WithOne(cr => cr.CourseEnrollment)
                .HasForeignKey<CourseResult>(cr => cr.EnrollmentId);
        }
    }
}
