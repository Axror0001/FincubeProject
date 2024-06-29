using Newtonsoft.Json;
using ProjectBlazor.DTO;
using ProjectBlazor.Models;
using System.Text;

namespace ProjectBlazor.StudentData
{
    public class StudentDto : IStudent
    {
        public readonly HttpClient _httpClient;

        public StudentDto(HttpClient httpClient)
        {
            this._httpClient = httpClient;

        }

        public async Task<List<Student>> GetAllStudent()
        {
            var httpResponce = await _httpClient.GetAsync("GetAllStudent/");
            var result = await httpResponce.Content.ReadAsStringAsync();
            var talaba = JsonConvert.DeserializeObject<List<Student>>(result);
            return talaba;
        }

        public async Task<Student> GetStudentById(int id)
        {
            var student = await _httpClient.GetFromJsonAsync<Student>($"GetStudentById/{id}");
            return student;

        }

        public async Task<Student> CreateStudent(CreateStudentDTO student)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(student), UnicodeEncoding.UTF8, "application/json");
            var content = await _httpClient.PostAsync("CreateStudent", stringContent);
            var result = JsonConvert.DeserializeObject<Student>(await content.Content.ReadAsStringAsync());
            return result;
        }

        public async Task<Student> DeleteStudent(int id)
        {
            return await _httpClient.DeleteFromJsonAsync<Student>($"DeleteStudent/{id}");
        }

        public async Task<Student> UpdateStudente(CreateStudentDTO student)
        {
            var responce = new StringContent(JsonConvert.SerializeObject(student), UnicodeEncoding.UTF8, "application/json");
            var result = await _httpClient.PutAsync($"UpdateStudent", responce);

            var students = JsonConvert.DeserializeObject<Student>(await result.Content.ReadAsStringAsync());

            return students;

        }
    }
}
