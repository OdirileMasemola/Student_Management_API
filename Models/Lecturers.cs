using System.Globalization;

namespace Student_Management_API.Models
{
    public class Lecturers
    {
        public int LecturerId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int PhoneNumber { get; set; }
    }
}
