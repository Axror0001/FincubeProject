using ProjectBlazor.DTO;
using ProjectBlazor.Models;

namespace ProjectBlazor.StudentData
{
    public interface IStudent
    {
        Task<List<Student>> GetAllStudent();

        Task<Student> GetStudentById(int id);

        Task<Student> CreateStudent(CreateStudentDTO student);

        Task<Student> DeleteStudent(int id);

        Task<Student> UpdateStudente(CreateStudentDTO student);
    }
}
