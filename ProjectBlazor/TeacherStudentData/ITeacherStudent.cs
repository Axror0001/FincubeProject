using ProjectBlazor.Models;

namespace ProjectBlazor.TeacherStudentData
{
    public interface ITeacherStudent
    {
        Task<List<TeacherStudent>> GetAlls();
        Task<List<Student>> GetTeachIdStudents(int teacherId);

        Task<List<Teacher>> GetStIdTeachers(int studentId);

        Task<TeacherStudent> CreateTeachStu(int studentId, int teacherId);

        Task<bool> DeleteTeachStu(int studentid, int teacherid);
    }
}
