using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Management_API.Data;
using Student_Management_API.Models;

namespace Student_Management_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly ILogger<StudentController> _logger;

        public StudentController(DatabaseContext context, ILogger<StudentController> logger)
        {
            _context = context;
            _logger = logger;
        }


        // Get all students

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Students>>> GetAllStudents()
        {
            try
            {
                var students = await _context.Students
                    .Include(s => s.Course)
                    .Include(s => s.Enrollments)
                    .ToListAsync();

                return Ok(students);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving students");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving students");
            }
        }


        // Get student by ID

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Students>> GetStudentById(int id)
        {
            try
            {
                var student = await _context.Students
                    .Include(s => s.Course)
                    .Include(s => s.Enrollments)
                    .FirstOrDefaultAsync(s => s.StudentId == id);

                if (student == null)
                {
                    return NotFound($"Student with ID {id} not found");
                }

                return Ok(student);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving student with ID {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving student");
            }
        }

        // Create a new student
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Students>> CreateStudent([FromBody] Students student)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Verify if course exists
                var courseExists = await _context.Courses.AnyAsync(c => c.CourseId == student.CourseId);
                if (!courseExists)
                {
                    return BadRequest("Course does not exist");
                }

                _context.Students.Add(student);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetStudentById), new { id = student.StudentId }, student);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating student");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating student");
            }
        }

        // Update a student
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Students>> UpdateStudent(int id, [FromBody] Students student)
        {
            try
            {
                if (id != student.StudentId)
                {
                    return BadRequest("Student ID mismatch");
                }

                var existingStudent = await _context.Students.FindAsync(id);
                if (existingStudent == null)
                {
                    return NotFound($"Student with ID {id} not found");
                }

                // Verify course exists if CourseId is being changed
                var courseExists = await _context.Courses.AnyAsync(c => c.CourseId == student.CourseId);
                if (!courseExists)
                {
                    return BadRequest("Course does not exist");
                }

                existingStudent.FirstName = student.FirstName;
                existingStudent.LastName = student.LastName;
                existingStudent.Email = student.Email;
                existingStudent.PhoneNumber = student.PhoneNumber;
                existingStudent.DateOfBirth = student.DateOfBirth;
                existingStudent.Address = student.Address;
                existingStudent.CourseId = student.CourseId;
                existingStudent.IsActive = student.IsActive;

                _context.Students.Update(existingStudent);
                await _context.SaveChangesAsync();

                return Ok(existingStudent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating student with ID {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating student");
            }
        }

        /// <summary>
        /// Delete a student
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteStudent(int id)
        {
            try
            {
                var student = await _context.Students.FindAsync(id);
                if (student == null)
                {
                    return NotFound($"Student with ID {id} not found");
                }

                _context.Students.Remove(student);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting student with ID {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting student");
            }
        }

        // Get students by course ID

        [HttpGet("course/{courseId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Students>>> GetStudentsByCourse(int courseId)
        {
            try
            {
                var students = await _context.Students
                    .Where(s => s.CourseId == courseId)
                    .Include(s => s.Course)
                    .ToListAsync();

                return Ok(students);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving students for course {courseId}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving students");
            }
        }
    }
}
