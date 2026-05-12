using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Management_API.Data;
using Student_Management_API.Models;

namespace Student_Management_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly ILogger<CourseController> _logger;

        public CourseController(DatabaseContext context, ILogger<CourseController> logger)
        {
            _context = context;
            _logger = logger;
        }
    }
}
