using Microsoft.EntityFrameworkCore;
using Assignment3.Models;

namespace Assignment3.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new ApplicationDBContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDBContext>>());

            // return if db is already seeded
            if (context.Courses.Any())
                return;

            // populates if db is empty
            context.Courses.AddRange(
                new Courses
                {
                    CourseID = "COSC2276",
                    Title = "DummyCourse",
                    CreditPoints = 8,
                    Career = "Undergraduate",
                    Coordinator = "Josh"
                },

                new Courses
                {
                    CourseID = "COSC2277",
                    Title = "DummyCourse2",
                    CreditPoints = 6,
                    Career = "Postgraduate",
                    Coordinator = "Chloe"
                });

            context.Students.AddRange(
                new Students
                {
                    StudentID = "s1234567",
                    FirstName = "Riz",
                    LastName = "Smith",
                    Email = "riz12smith@gmail.com"
                },

                new Students
                {
                    StudentID = "s7654321",
                    FirstName = "Rachel",
                    LastName = "Green",
                    Email = "rachelgreen@gmail.com",
                    MobilePhone = "0412345678"
                });

            context.SaveChanges();

        }
    }
}
