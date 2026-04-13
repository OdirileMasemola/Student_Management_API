using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Student_Management_API.Models
{
    public class Courses
    {
        [Key]
        public int CourseId { get; set; }

        [Required]
        [StringLength(150)]
        public string CourseName { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string CourseCode { get; set; } = string.Empty;

        [Range(0.5, 48)]
        public double DurationInMonths { get; set; }

        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        [Range(0, 1000)]
        public decimal Credits { get; set; }

        // foreign key
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }

        [ForeignKey("Lecturer")]
        public int LecturerId { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public bool IsActive { get; set; } = true;

        // Nav Properties
        public virtual Department Department { get; set; }

        public virtual Lecturers Lecturer { get; set; }

        public virtual ICollection<Students> Students { get; set; } = new List<Students>();

        public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
