using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentEnrollmentBackend.Data;
using StudentEnrollmentBackend.Models;
using StudentEnrollmentBackend.DTOs;

namespace StudentEnrollmentBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CourseController(AppDbContext context) {
            _context = context;

        }

        [HttpPost]
        public async Task<IActionResult> AddCourse(CreateCourseDto dto) {
            var course = new Course
            {
                CourseName = dto.CourseName,
                CreditHours = dto.CreditHours
            };
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            return Ok(course);
            
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCourses() {
            var courses = await _context.Courses
                .Select(c=>new CourseDto
                { 
                    Id=c.Id,
                    CourseName=c.CourseName,
                    CreditHours=c.CreditHours
                }).ToListAsync();
            return Ok(courses);

        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetCourseById(int Id) {
            var course = await _context.Courses
                .Where(c => c.Id == Id)
                .Select(c=> new CourseDto { 
                    Id=c.Id,
                    CourseName=c.CourseName,
                    CreditHours=c.CreditHours
                })
                .FirstOrDefaultAsync();
            if (course == null) {
                return NotFound("Course doesnt exist");
            }
            return Ok(course);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateCourse(int Id, UpdateCourseDto dto) {
            var course = await _context.Courses
                .Where(s=>s.Id==Id)
                .FirstOrDefaultAsync(c => c.Id == Id);
            if (course == null) {
                return NotFound("Course Doesnt Exist");
            }
            course.CourseName = dto.CourseName;
            course.CreditHours = dto.CreditHours;
            await _context.SaveChangesAsync();
            return Ok(course);

        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteCourse(int Id) {
            var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == Id);
            if (course == null) {
                return NotFound("Course Doesnt Exist");
            }
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return Ok("Course Deleted Sucessfully");
        }
        
    }
}
