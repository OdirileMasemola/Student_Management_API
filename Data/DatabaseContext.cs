using Microsoft.EntityFrameworkCore;
using Student_Management_API.Models;

namespace Student_Management_API.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options ) : base(options)
        {
            
        }
        public DbSet <Courses> Courses { get; set; }
        public DbSet <Department> Departments { get; set; }
        public DbSet <Enrollment> Enrollments { get; set; }
        public DbSet <Lecturers> Lecturers{ get; set; }
        public DbSet <Students> Students { get; set; }
    }
}
