using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentEnrollmentBackend.Data;
using StudentEnrollmentBackend.Models;
using StudentEnrollmentBackend.DTOs;

namespace StudentEnrollmentBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnrollmentController : ControllerBase
    {
        private readonly AppDbContext _context;
        public EnrollmentController(AppDbContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEnrollments() {
            var enrollments = await _context.Enrollments
                .Select(e => new EnrollmentDto
                {
                   Id=e.Id,
                   EnrollmentDate =e.EnrollmentDate,
                   CourseGrade=e.CourseGrade,

                    Student = new StudentDto
                    {
                        Id=e.Student.Id,
                        Name=e.Student.Name
                    },
                    Course = new CourseDto
                    {
                        Id=e.Course.Id,
                        CourseName=e.Course.CourseName,
                        CreditHours=e.Course.CreditHours

                    }
                }
                ).ToListAsync();
            return Ok(enrollments);

        
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetEnrollmentById(int Id) {
            var enrollment = await _context.Enrollments
                .Select(e => new EnrollmentDto {
                    Id=e.Id,
                    EnrollmentDate=e.EnrollmentDate,
                    CourseGrade=e.CourseGrade,

                    Student = new StudentDto{
                        Id=e.Student.Id,
                        Name=e.Student.Name
                    },
                    Course = new CourseDto{
                        Id=e.Course.Id,
                        CourseName=e.Course.CourseName,
                        CreditHours=e.Course.CreditHours
                    }
                }
                ).FirstOrDefaultAsync(e=>e.Id==Id);
            if (enrollment == null) {
                return NotFound("Enrollment Not Found");
            }

            return Ok(enrollment);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEnrollment(CreateEnrollmentDto dto) {
            var StudentExists = await _context.Students.FirstOrDefaultAsync(s => s.Id == dto.StudentId);
            if (StudentExists==null) {
                return NotFound("Student Not Found");
            }
            var CourseExists = await _context.Courses.FirstOrDefaultAsync(c => c.Id == dto.CourseId);
            if (CourseExists==null)
            {
                return NotFound("Course Not Found");
            }

            var enrollment = new Enrollment
            {
                StudentId = dto.StudentId,
                CourseId = dto.CourseId,
                EnrollmentDate = dto.EnrollmentDate,
                CourseGrade = dto.CourseGrade
            };
            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                enrollment.Id,
                enrollment.StudentId,
                enrollment.CourseId,
                enrollment.EnrollmentDate,
                enrollment.CourseGrade
            }); 


        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateEnrollment(int Id, UpdateEnrollmentDto dto) {
            var enrollment = await _context.Enrollments.FirstOrDefaultAsync(e => e.Id == Id);
            if (enrollment == null) {
                return NotFound("Enrollment Not Found");
            
            }
            var StudentExists = await _context.Students.FirstOrDefaultAsync(s => s.Id == dto.StudentId);
            if (StudentExists == null)
            {
                return NotFound("Student Not Found");
            }
            var CourseExists = await _context.Courses.FirstOrDefaultAsync(c => c.Id == dto.CourseId);
            if (CourseExists == null)
            {
                return NotFound("Course Not Found");
            }
            enrollment.StudentId = dto.StudentId;
            enrollment.CourseId = dto.CourseId;
            enrollment.CourseGrade = dto.CourseGrade;
            await _context.SaveChangesAsync();
            return Ok(new
            {
                enrollment.Id,
                enrollment.StudentId,
                enrollment.CourseId,
                enrollment.EnrollmentDate,
                enrollment.CourseGrade
            }); 
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteEnrollment(int Id) {
            var enrollment = await _context.Enrollments.FirstOrDefaultAsync(e => e.Id == Id);
            if (enrollment == null)
            {
                return NotFound("Enrollment Not Found");
            }
            _context.Enrollments.Remove(enrollment);
            await _context.SaveChangesAsync();
            return Ok("Enrollment Deleted Successfully");
        }

        
    }
}

