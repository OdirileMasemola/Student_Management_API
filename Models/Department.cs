using System.ComponentModel.DataAnnotations;

namespace Student_Management_API.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        [Required]
        [StringLength(100)]
        public string DepartmentName { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string DepartmentCode { get; set; } = string.Empty;

        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        [StringLength(100)]
        public string HeadName { get; set; } = string.Empty;

        [EmailAddress]
        public string ContactEmail { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public bool IsActive { get; set; } = true;

        // Nav Properties
        public virtual ICollection<Lecturers> Lecturers { get; set; } = new List<Lecturers>();

        public virtual ICollection<Courses> Courses { get; set; } = new List<Courses>();
    }
}
