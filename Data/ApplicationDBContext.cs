using Microsoft.EntityFrameworkCore;
using Assignment3.Models;

namespace Assignment3.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        { }

        public DbSet<Students> Students { get; set; }
        public DbSet<Courses> Courses { get; set; }
        public DbSet<Enrolled> Enrollments { get; set; }

        // check contraints for models
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Students>(entity => entity.ToTable("Students", t =>
            {
                t.HasCheckConstraint("CK_Student_StudentID", "StudentID LIKE 's[0-9][0-9][0-9][0-9][0-9][0-9][0-9]'");
                t.HasCheckConstraint("CK_Student_FirstName", "FirstName LIKE '[A-Z]%' AND FirstName NOT LIKE '%[^a-zA-Z]%'");
                t.HasCheckConstraint("CK_Student_LastName", "LastName LIKE '[A-Z]%' AND LastName NOT LIKE '%[^a-zA-Z]%'");
                t.HasCheckConstraint("CK_Student_Email", "Email LIKE '%@%'");
                t.HasCheckConstraint("CK_Student_MobilePhone", "MobilePhone LIKE '04[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'");
            }));

            builder.Entity<Courses>(entity => entity.ToTable("Courses", t =>
            {
                t.HasCheckConstraint("CK_Course_CourseID", "CourseID LIKE 'COSC[0-9][0-9][0-9][0-9]'");
                t.HasCheckConstraint("CK_Course_Career", "Career IN ('Undergraduate', 'Postgraduate')");
                t.HasCheckConstraint("CK_Course_Coordinator", "Coordinator LIKE '[A-Z]%' AND Coordinator NOT LIKE '%[^a-zA-Z ]%'");
            }));

            builder.Entity<Enrolled>(entity => entity.ToTable("Enrolled", t =>
            {
                entity.HasOne(e => e.Student).WithMany(e => e.Enrollments).HasForeignKey(e => e.StudentID);
                entity.HasOne(e => e.Course).WithMany(e => e.Enrollments).HasForeignKey(e => e.CourseID);
            }));
        }
    }
}
