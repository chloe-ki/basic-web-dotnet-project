using Microsoft.AspNetCore.Mvc;
using Assignment3.Data;
using Assignment3.ViewModels;
using Assignment3.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment3.Controllers
{
    public class CourseController : Controller
    {
        private readonly ApplicationDBContext _context;

        public CourseController(ApplicationDBContext context) => _context = context;

        public async Task<IActionResult> ViewCourse()
        {
            // receive data
            var courses = await _context.Courses
            .Include(c => c.Enrollments)
            .ThenInclude(e => e.Student)
            .Select(c => new CourseViewModel
            {
                CourseID = c.CourseID,
                Title = c.Title,
                CreditPoints = c.CreditPoints,
                Career = c.Career,
                Coordinator = c.Coordinator,
                EnrolledStudents = c.Enrollments.Select(e => new StudentViewModel
                {
                    StudentID = e.Student.StudentID,
                    FirstName = e.Student.FirstName,
                    LastName = e.Student.LastName,
                    Email = e.Student.Email,
                    MobilePhone = e.Student.MobilePhone
                }).ToList()
            })
            .ToListAsync();

            return View(courses);
        }
        public IActionResult CreateCourse()
        {
            return View();
        }

        // when submitting create course form
        [HttpPost]
        public async Task<IActionResult> CreateCourse([Bind("CourseID,Title,CreditPoints,Career,Coordinator")] Courses course)
        {
            // adds course to database, displays error message if invalid
            try
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Successfully created new course!";
                return RedirectToAction(nameof(ViewCourse));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Please enter valid items in all fields.");

                return View(course);
            }
        }
    }
}
