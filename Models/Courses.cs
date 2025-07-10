using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment3.Models
{
    public class Courses
    {
        [Key, StringLength(8)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string CourseID { get; set; }

        [Required, StringLength(100)]
        public string Title { get; set; }

        [Required, Range(1, 12)]
        public int CreditPoints { get; set; }

        [Required, StringLength(30)]
        public string Career { get; set; }

        [Required, StringLength(50)]
        public string Coordinator { get; set; }

        public virtual List<Enrolled> Enrollments { get; set; }
    }
}
