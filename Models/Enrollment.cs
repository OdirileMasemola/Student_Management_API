using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Student_Management_API.Models
{
    public class Enrollment
    {
        [Key]
        public int EnrollmentId { get; set; }

        public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;

        public DateTime? CompletionDate { get; set; }

        [StringLength(50)]
        public string Status { get; set; } = "Inactive"; 

        [Range(0, 100)]
        public double? GradePercentage { get; set; }

        [StringLength(2)]
        public string LetterGrade { get; set; } = string.Empty;
        //foreign keys
        [ForeignKey("Student")]
        public int StudentId { get; set; }

        [ForeignKey("Course")]
        public int CourseId { get; set; }

        // Navigation Properties
        public virtual Students Student { get; set; }

        public virtual Courses Course { get; set; }
    }
}
