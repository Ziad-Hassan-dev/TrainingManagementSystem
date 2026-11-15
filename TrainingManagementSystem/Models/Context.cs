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
            //Unique Indexes
            modelBuilder.Entity<Department>().HasIndex(d => d.Name).IsUnique();
            modelBuilder.Entity<Trainee>().HasIndex(t => t.Email).IsUnique();
            modelBuilder.Entity<Instructor>().HasIndex(i => i.Email).IsUnique();
            modelBuilder.Entity<Course>()
                .HasIndex(c => new { c.Name, c.DeptId }).IsUnique();
            modelBuilder.Entity<CourseEnrollment>()
                .HasIndex(e => new { e.CourseId, e.TraineeId })
                .IsUnique()
                .HasFilter("[Status] <> 2");
            modelBuilder.Entity<Department>()
                .Property(d => d.Name).HasMaxLength(50);
            modelBuilder.Entity<Department>()
                .Property(d => d.Description).HasMaxLength(200);

            modelBuilder.Entity<Trainee>()
                .Property(t => t.Name).HasMaxLength(100);
            modelBuilder.Entity<Trainee>()
                .Property(t => t.Email).HasMaxLength(100);
            modelBuilder.Entity<Trainee>()
                .Property(t => t.Address).HasMaxLength(200);
            modelBuilder.Entity<Trainee>()
                .Property(t => t.ImageURL).HasMaxLength(300);

            modelBuilder.Entity<Instructor>()
                .Property(i => i.Name).HasMaxLength(100);
            modelBuilder.Entity<Instructor>()
                .Property(i => i.Email).HasMaxLength(100);
            modelBuilder.Entity<Instructor>()
                .Property(i => i.Address).HasMaxLength(200);
            modelBuilder.Entity<Instructor>()
                .Property(i => i.ImageURL).HasMaxLength(300);

            modelBuilder.Entity<Course>()
                .Property(c => c.Name).HasMaxLength(100);
            modelBuilder.Entity<Course>()
                .Property(c => c.Description).HasMaxLength(500);
        }
    }
}
