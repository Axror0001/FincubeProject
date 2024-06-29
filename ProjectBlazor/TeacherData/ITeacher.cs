using ProjectBlazor.DTO;
using ProjectBlazor.Models;

namespace ProjectBlazor.TeacherData
{
    public interface ITeacher
    {
        Task<List<Teacher>> GetAllTeacher();

        Task<Teacher> GetByIdTeacher(int id);

        Task<Teacher> CreateTeacher(CreateTeacherDto teacher);


        Task<Teacher> DeleteTeacher(int id);

        Task<Teacher> UpdateTeacheres(CreateTeacherDto teacher);
    }
}
