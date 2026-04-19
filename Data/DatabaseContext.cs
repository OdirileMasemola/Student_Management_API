using Microsoft.EntityFrameworkCore;
using Student_Management_API.Models;

namespace Student_Management_API.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<Courses> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Lecturers> Lecturers { get; set; }
        public DbSet<Students> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuring Department
            modelBuilder.Entity<Department>()
                .HasKey(d => d.DepartmentId);
            modelBuilder.Entity<Department>()
                .HasIndex(d => d.DepartmentCode)
                .IsUnique();
            modelBuilder.Entity<Department>()
                .Property(d => d.DepartmentName)
                .IsRequired()
                .HasMaxLength(100);

            // configuring Lecturers
            modelBuilder.Entity<Lecturers>()
                .HasKey(l => l.LecturerId);
            modelBuilder.Entity<Lecturers>()
                .HasIndex(l => l.Email)
                .IsUnique();
            modelBuilder.Entity<Lecturers>()
                .HasOne(l => l.Department)
                .WithMany(d => d.Lecturers)
                .HasForeignKey(l => l.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            //Courses
            modelBuilder.Entity<Courses>()
                .HasKey(c => c.CourseId);
            modelBuilder.Entity<Courses>()
                .HasIndex(c => c.CourseCode)
                .IsUnique();
            modelBuilder.Entity<Courses>()
                .HasOne(c => c.Department)
                .WithMany(d => d.Courses)
                .HasForeignKey(c => c.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Courses>()
                .HasOne(c => c.Lecturer)
                .WithMany(l => l.Courses)
                .HasForeignKey(c => c.LecturerId)
                .OnDelete(DeleteBehavior.Restrict);

            //Students
            modelBuilder.Entity<Students>()
                .HasKey(s => s.StudentId);
            modelBuilder.Entity<Students>()
                .HasIndex(s => s.Email)
                .IsUnique();
            modelBuilder.Entity<Students>()
                .HasOne(s => s.Course)
                .WithMany(c => c.Students)
                .HasForeignKey(s => s.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            // Enrollment 
            modelBuilder.Entity<Enrollment>()
                .HasKey(e => e.EnrollmentId);
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Enrollment>()
                .HasIndex(e => new { e.StudentId, e.CourseId })
                .IsUnique()
                .HasName("IX_Enrollment_StudentId_CourseId");
        }
    }
}
