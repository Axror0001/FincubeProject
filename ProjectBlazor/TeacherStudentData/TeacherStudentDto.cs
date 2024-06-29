using Newtonsoft.Json;
using ProjectBlazor.Models;

namespace ProjectBlazor.TeacherStudentData
{
    public class TeacherStudentDto : ITeacherStudent
    {
        public readonly HttpClient _httpClient;
        public TeacherStudentDto(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task<List<Teacher>> GetStIdTeachers(int studentId)
        {
            try
            {
                var responce = await _httpClient.GetAsync($"GetStIdTeachers/{studentId}");
                var result = await responce.Content.ReadAsStringAsync();
                var requred = JsonConvert.DeserializeObject<List<Teacher>>(result);

                return requred;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

        }

        public async Task<List<Student>> GetTeachIdStudents(int teacherId)
        {
            try
            {
                var responce = await _httpClient.GetAsync($"GetTeachIdStudents/{teacherId}");
                var result = await responce.Content.ReadAsStringAsync();
                var stAll = JsonConvert.DeserializeObject<List<Student>>(result);

                return stAll;
            }
            catch (Exception msg)
            {
                Console.WriteLine(msg.Message);
                throw;
            }
        }
        public async Task<TeacherStudent> CreateTeachStu(int studentId, int teacherId)
        {
            TeacherStudent ts = new TeacherStudent();
            ts.StudentId = studentId;
            ts.TeacherId = teacherId;
            await _httpClient.PostAsync($"CreateTeachStu?studentId={studentId}&teacherId={teacherId}", null);
            return ts;
        }
        public async Task<bool> DeleteTeachStu(int studentid, int teacherid)
        {
            var responce = await _httpClient.DeleteAsync($"DeleteTeachIdStu?stId={studentid}&teachId={teacherid}");
            if (responce.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<TeacherStudent>> GetAlls()
        {
            var request = await _httpClient.GetAsync("GetAll/");
            var responce = await request.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<TeacherStudent>>(responce);

            return result;
        }
    }
}
