using Course_Service.dto;
using Course_Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Course_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        // POST: api/Course
        [HttpPost]
        public async Task<IActionResult> AddCourse(CourseRequest courseRequest)
        {
            await _courseService.AddCourseAsync(courseRequest);
            return Ok("Course added successfully.");
        }

        // GET: api/Course
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseResponse>>> GetAllCourses()
        {
            var courses = await _courseService.GetAllCourseAsync();
            return Ok(courses);
        }

        // GET: api/Course/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseResponse>> GetCourseById(int id)
        {
            try
            {
                var course = await _courseService.GetCourseById(id);
                return Ok(course);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // PUT: api/Course/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, CourseRequest courseRequest)
        {
            try
            {
                await _courseService.UpdateCourseByIdAsync(id, courseRequest);
                return Ok("Course updated successfully.");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DELETE: api/Course/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            try
            {
                await _courseService.DeleteCourseByIdAsync(id);
                return Ok("Course deleted successfully.");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
