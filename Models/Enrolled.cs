using Microsoft.EntityFrameworkCore;

namespace Assignment3.Models
{
    [PrimaryKey(nameof(CourseID), nameof(StudentID))]
    public class Enrolled
    {
        public string CourseID { get; set; }
        public virtual Courses Course { get; set; }

        public string StudentID { get; set; }
        public virtual Students Student { get; set; }
    }
}
