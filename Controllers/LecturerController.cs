using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Management_API.Data;
using Student_Management_API.Models;

namespace Student_Management_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LecturerController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly ILogger<LecturerController> _logger;

        public LecturerController(DatabaseContext context, ILogger<LecturerController> logger)
        {
            _context = context;
            _logger = logger;
        }
    }
}
