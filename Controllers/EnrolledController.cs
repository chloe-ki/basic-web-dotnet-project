using Microsoft.AspNetCore.Mvc;
using Assignment3.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assignment3.ViewModels;
using Assignment3.Models;

namespace Assignment3.Controllers
{
    public class EnrolledController : Controller
    {
        private readonly ApplicationDBContext _context;

        public EnrolledController(ApplicationDBContext context)
        {
            _context = context;
        }
        public IActionResult EnrollStudent()
        {
            var viewModel = CreateViewModel();

            return View(viewModel);
        }

        // retrieve data from context into view model
        public StudentEnrollmentViewModel CreateViewModel(StudentEnrollmentViewModel viewModel = null)
        {
            return new StudentEnrollmentViewModel
            {
                Courses = _context.Courses
                    .Select(c => new SelectListItem { Value = c.CourseID, Text = c.Title })
                    .ToList(),
                Students = _context.Students
                    .Select(s => new SelectListItem { Value = s.StudentID, Text = $"{s.FirstName} {s.LastName}" })
                    .ToList(),
                SelectedCourseID = viewModel?.SelectedCourseID,
                SelectedStudentID = viewModel?.SelectedStudentID
            };
        }

        // upon enrolling student submission
        [HttpPost]
        public async Task<IActionResult> EnrollStudent(StudentEnrollmentViewModel viewModel)
        {
            // adds to db is valid
            try
            {
                var existingEnrollment = await _context.Enrollments
                    .AnyAsync(e => e.CourseID == viewModel.SelectedCourseID && e.StudentID == viewModel.SelectedStudentID);

                if (existingEnrollment)
                {
                    ModelState.AddModelError("", "The student is already enrolled in this course.");

                    return View(viewModel);
                }

                var enrollment = new Enrolled
                {
                    CourseID = viewModel.SelectedCourseID,
                    StudentID = viewModel.SelectedStudentID
                };

                // reloads with confirmation message
                _context.Add(enrollment);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Successfully enrolled!";
                return RedirectToAction(nameof(EnrollStudent));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Please make a selection.");
                return View(viewModel);
            }
        }

    }
}
