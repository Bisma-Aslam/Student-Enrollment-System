using Microsoft.AspNetCore.Hosting.Server;

namespace StudentEnrollmentBackend.DTOs
{
    public class CreateCourseDto
    {
        public string CourseName { get; set; }
        public int CreditHours { get; set; }
    }
}
