namespace StudentEnrollmentBackend.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string CourseName { get; set; }
        public int CreditHours { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
