using Course_Service.dto;

namespace Course_Service.Interface;

public interface ICourseService
{
    Task AddCourseAsync(CourseRequest courseRequest);
    Task<IEnumerable<CourseResponse>> GetAllCourseAsync();
    Task<CourseResponse> GetCourseById(int id);
    Task UpdateCourseByIdAsync(int id, CourseRequest courseRequest);
    Task DeleteCourseByIdAsync(int id);
}