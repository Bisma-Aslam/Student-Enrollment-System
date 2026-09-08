using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentEnrollmentBackend.Models;
using StudentEnrollmentBackend.Data;
using StudentEnrollmentBackend.DTOs;
using Microsoft.EntityFrameworkCore;


namespace StudentData.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase

    {
        private readonly AppDbContext _context;

        public StudentController(AppDbContext context)
        {
            _context = context;
        
        }
        //private static List<Student> Students = new List<Student>
        //{
        //    new Student {Id=1, Name="Ali Khan", Age=20},
        //    new Student {Id=2, Name="Ahmad Raza", Age=22},
        //    new Student {Id=3, Name="Zara Larson", Age=24}

        //};

        //GET all Students
        //[Authorize(Roles = "User")]
        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var students = await _context.Students
                .Select(s => new StudentDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Age = s.Age
                }).ToListAsync();
                
            return Ok(students);
        }

        //Get Student by id
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetStudentById(int Id)
        {
            var student = await _context.Students
                .Where(s => s.Id == Id)
                .Select(s => new StudentDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Age = s.Age
                }).FirstOrDefaultAsync();
            if (student == null)
            {
                return NotFound("Student doesn't exist");
            }
            return Ok(student);
        }
        //Post

        [HttpPost]
        public async Task<IActionResult> AddStudent(CreateStudentDto dto)
        {
            var student = new Student
            {
                Name = dto.Name,
                Age = dto.Age
            };
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return Ok("Student added successfully");
        }
        //PUT
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateStudent(int Id, UpdateStudentDto dto)
        {
            var Student = await _context.Students
                .Where(s => s.Id == Id)
                .FirstOrDefaultAsync();
            if (Student == null)
            {
                return NotFound("Student doesnt exist");
            }
            Student.Name = dto.Name;
            Student.Age = dto.Age;
            await _context.SaveChangesAsync();
            return Ok("Student updated sucessfully");
        }

        //Delete
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteStudent(int Id)
        {
            var Student = await _context.Students
                .Where(s=>s.Id==Id)
                .FirstOrDefaultAsync();
            if (Student == null)
            {
                return NotFound("Student doesnt exist");
            }
            _context.Students.Remove(Student);
            await _context.SaveChangesAsync();
            return Ok("Student deleted sucessfully");
        }
        //[Authorize(Roles = "Admin")]
        [HttpDelete("admin-test")]
        public IActionResult AdminTest()
        {
            return Ok("You are an Admin");
        }

        [HttpGet("above28")]
        public async Task<IActionResult> StudentsAbove28() 
        {
            var students = await _context.Students
                .Where(s => s.Age > 28)
                .ToListAsync();
            return Ok(students);
                
        }


    }
}