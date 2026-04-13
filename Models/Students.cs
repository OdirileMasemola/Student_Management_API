using System.ComponentModel.DataAnnotations.Schema;

namespace Student_Management_API.Models
{
    public class Students
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        
        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public string Course { get; set; } = string.Empty;

    }
}
