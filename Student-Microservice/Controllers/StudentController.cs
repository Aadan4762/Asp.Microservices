using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Student_Microservice.Dto;
using Student_Microservice.Interface;

namespace Student_Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetAllStudents()
        {
            var students = await _studentService.GetAllStudentsAsync();
            if (students == null || !students.Any())
            {
                return NotFound("No Students found.");
            }
            return Ok(new { message = "Students retrieved successfully.", data = students });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDto>> GetStudentById(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound($"Student with ID {id} not found.");
            }

            return Ok(new { message = $"Student with ID {id} retrieved successfully.", data = student });
        }

        [HttpPost]
        public async Task<ActionResult> AddStudent([FromBody] StudentDto studentDto)
        {
            try
            {
                await _studentService.AddStudentAsync(studentDto);
                return Ok(new { message = "Student added successfully.", data = studentDto });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "An error occurred while adding the student.", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] StudentDto studentDto)
        {
            try
            {
                await _studentService.UpdateStudentAsync(id, studentDto);
                return Ok(new { message = $"Student with ID {id} updated successfully." });
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Student with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the student.", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            try
            {
                await _studentService.DeleteStudentAsync(id);
                return Ok(new { message = $"Student with ID {id} deleted successfully." });
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Student with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the student.", error = ex.Message });
            }
        }
    }
}
