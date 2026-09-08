namespace StudentEnrollmentBackend.DTOs
{
    public class EnrollmentDto
    {
        public int Id { get; set; }
       public DateTime EnrollmentDate { get; set; }
        public string CourseGrade { get; set; }
        public StudentDto Student { get; set; }
        public CourseDto Course { get; set; }
    }

   
    
    
}
