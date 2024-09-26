using Course_Service.dto;
using Course_Service.Entity;
using Course_Service.Interface;

namespace Course_Service.Implementation;

public class CourseService : ICourseService
{
    private readonly IRepository<Course> _courseRepository;

    public CourseService(IRepository<Course> courseRepository)
    {
        _courseRepository = courseRepository;
    }

    // Add a new course
    public async Task AddCourseAsync(CourseRequest courseRequest)
    {
        var course = MapToCourseEntity(courseRequest);
        await _courseRepository.AddAsync(course);
        await _courseRepository.SaveChangesAsync();
    }

    // Retrieve all courses
    public async Task<IEnumerable<CourseResponse>> GetAllCourseAsync()
    {
        var courses = await _courseRepository.GetAllAsync();
        return courses.Select(course => MapToCourseResponse(course)).ToList();
    }

    // Get course by id
    public async Task<CourseResponse> GetCourseById(int id)
    {
        var course = await _courseRepository.GetByIdAsync(id);
        if (course == null)
        {
            throw new KeyNotFoundException($"Course with id {id} not found.");
        }
        return MapToCourseResponse(course);
    }

    // Update course by id
    public async Task UpdateCourseByIdAsync(int id, CourseRequest courseRequest)
    {
        var course = MapToCourseEntity(courseRequest);
        await _courseRepository.UpdateAsync(id, course);
        await _courseRepository.SaveChangesAsync();
    }

    // Delete course by id
    public async Task DeleteCourseByIdAsync(int id)
    {
        await _courseRepository.DeleteAsync(id);
        await _courseRepository.SaveChangesAsync();
    }

    // Map DTO to Entity
    private Course MapToCourseEntity(CourseRequest courseRequest)
    {
        return new Course
        {
            courseName = courseRequest.courseName,
            courseCode = courseRequest.courseCode
        };
    }

    // Map Entity to DTO
    private CourseResponse MapToCourseResponse(Course course)
    {
        return new CourseResponse
        {
            id = course.id,
            courseName = course.courseName,
            courseCode = course.courseCode
        };
    }
}
