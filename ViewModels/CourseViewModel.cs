namespace Assignment3.ViewModels
{
    public class CourseViewModel
    {
        public string CourseID { get; set; }
        public string Title { get; set; }
        public int CreditPoints { get; set; }
        public string Career { get; set; }
        public string Coordinator { get; set; }
        public List<StudentViewModel> EnrolledStudents { get; set; }
    }
}
