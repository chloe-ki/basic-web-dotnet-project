using Assignment3.Data;
using Assignment3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assignment3.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsApiController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public StudentsApiController(ApplicationDBContext context)
        {
            _context = context;
        }

        // gets student data from context 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Students>>> GetStudents()
        {
            return await _context.Students.ToListAsync();
        }

    }
}