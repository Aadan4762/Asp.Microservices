
using Student_Microservice.Dto;
using Student_Microservice.Interface;
using Student_Microservice.Models;

namespace Student_Microservice.Services
{
    public class StudentService : IStudentService
    {
        private readonly IRepository<Student> _studentRepository;

        public StudentService(IRepository<Student> studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<StudentDto> GetStudentByIdAsync(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            if (student == null)
            {
                return null;
            }

            return MapToStudentDto(student);
        }

        public async Task<IEnumerable<StudentDto>> GetAllStudentsAsync()
        {
            var students = await _studentRepository.GetAllAsync();
            return students.Select(s => MapToStudentDto(s));
        }

        public async Task AddStudentAsync(StudentDto studentDto)
        {
            var student = MapToStudentEntity(studentDto);
            await _studentRepository.AddAsync(student);
            await SaveChangesAsync(); // Save changes after adding
        }

        public async Task UpdateStudentAsync(int id, StudentDto studentDto)
        {
            var student = MapToStudentEntity(studentDto);
            await _studentRepository.UpdateAsync(id, student);
            await SaveChangesAsync(); // Save changes after updating
        }

        public async Task DeleteStudentAsync(int id)
        {
            await _studentRepository.DeleteAsync(id);
            await SaveChangesAsync(); // Save changes after deleting
        }

        public async Task SaveChangesAsync()
        {
            await _studentRepository.SaveChangesAsync();
        }

        private StudentDto MapToStudentDto(Student student)
        {
            return new StudentDto
            {
                id = student.id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Age = student.Age,
                Email = student.Email
            };
        }

        private Student MapToStudentEntity(StudentDto studentDto)
        {
            return new Student
            {
                id = studentDto.id,
                FirstName = studentDto.FirstName,
                LastName = studentDto.LastName,
                Age = studentDto.Age,
                Email = studentDto.Email
            };
        }
    }
}
