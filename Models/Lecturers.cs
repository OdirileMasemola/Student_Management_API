using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Student_Management_API.Models
{
    public class Lecturers
    {
        [Key]
        public int LecturerId { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;

        [StringLength(50)]
        public string EmployeeId { get; set; } = string.Empty;

        [StringLength(100)]
        public string Specialization { get; set; } = string.Empty;

        public DateTime HireDate { get; set; } = DateTime.UtcNow;

        // foreign key
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }

        public bool IsActive { get; set; } = true;

        //Nav Properties
        public virtual Department Department { get; set; }

        public virtual ICollection<Courses> Courses { get; set; } = new List<Courses>();
    }
}
