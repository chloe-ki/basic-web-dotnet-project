using Microsoft.AspNetCore.Mvc.Rendering;

namespace Assignment3.ViewModels
{
    public class StudentEnrollmentViewModel
    {
        public string SelectedCourseID { get; set; }
        public string SelectedStudentID { get; set; }

        public IEnumerable<SelectListItem> Courses { get; set; }
        public IEnumerable<SelectListItem> Students { get; set; }
    }
}
