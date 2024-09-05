using Student_Microservice.Dto;

namespace Student_Microservice.Interface;

public interface IStudentService
{
    Task<StudentDto> GetStudentByIdAsync(int id);
    Task<IEnumerable<StudentDto>> GetAllStudentsAsync();
    Task AddStudentAsync(StudentDto studentDto);
    Task UpdateStudentAsync(int id, StudentDto studentDto);
    Task DeleteStudentAsync(int id); 
    Task SaveChangesAsync(); 
}
