using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment3.Models
{
    public class Students
    {
        [Key, StringLength(8)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string StudentID { get; set; }

        [Required, StringLength(30)]
        public string FirstName { get; set; }

        [Required, StringLength(30)]
        public string LastName { get; set; }

        [Required, StringLength(320)]
        public string Email { get; set; }

        [StringLength(10)]
        public string MobilePhone { get; set; }

        public virtual List<Enrolled> Enrollments { get; set; }
    }
}
