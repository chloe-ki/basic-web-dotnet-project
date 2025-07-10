using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment3.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CourseID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreditPoints = table.Column<int>(type: "int", nullable: false),
                    Career = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Coordinator = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseID);
                    table.CheckConstraint("CK_Course_Career", "Career IN ('Undergraduate', 'Postgraduate')");
                    table.CheckConstraint("CK_Course_Coordinator", "Coordinator LIKE '[A-Z]%' AND Coordinator NOT LIKE '%[^a-zA-Z ]%'");
                    table.CheckConstraint("CK_Course_CourseID", "CourseID LIKE 'COSC[0-9][0-9][0-9][0-9]'");
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(320)", maxLength: 320, nullable: false),
                    MobilePhone = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentID);
                    table.CheckConstraint("CK_Student_Email", "Email LIKE '%@%'");
                    table.CheckConstraint("CK_Student_FirstName", "FirstName LIKE '[A-Z]%' AND FirstName NOT LIKE '%[^a-zA-Z]%'");
                    table.CheckConstraint("CK_Student_LastName", "LastName LIKE '[A-Z]%' AND LastName NOT LIKE '%[^a-zA-Z]%'");
                    table.CheckConstraint("CK_Student_MobilePhone", "MobilePhone LIKE '04[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'");
                    table.CheckConstraint("CK_Student_StudentID", "StudentID LIKE 's[0-9][0-9][0-9][0-9][0-9][0-9][0-9]'");
                });

            migrationBuilder.CreateTable(
                name: "Enrolled",
                columns: table => new
                {
                    CourseID = table.Column<string>(type: "nvarchar(8)", nullable: false),
                    StudentID = table.Column<string>(type: "nvarchar(8)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrolled", x => new { x.CourseID, x.StudentID });
                    table.ForeignKey(
                        name: "FK_Enrolled_Courses_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Courses",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enrolled_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Enrolled_StudentID",
                table: "Enrolled",
                column: "StudentID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Enrolled");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
