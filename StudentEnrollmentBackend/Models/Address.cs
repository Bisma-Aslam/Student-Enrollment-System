namespace StudentEnrollmentBackend.Models
{
    public class Address
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }

        public Student Student { get; set; }
        
    }
}
