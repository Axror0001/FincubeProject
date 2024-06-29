using Newtonsoft.Json;
using ProjectBlazor.DTO;
using ProjectBlazor.Models;
using System.Text;

namespace ProjectBlazor.TeacherData
{
    public class TeacherDto : ITeacher
    {
        public readonly HttpClient _httpClient;
        public TeacherDto(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }
        public async Task<List<Teacher>> GetAllTeacher()
        {
            var responce = await _httpClient.GetAsync("GetAllTeacher/");
            var result = await responce.Content.ReadAsStringAsync();
            var teacher = JsonConvert.DeserializeObject<List<Teacher>>(result);

            return teacher;
        }

        public async Task<Teacher> GetByIdTeacher(int id)
        {
            return await _httpClient.GetFromJsonAsync<Teacher>($"GetByIdTeacher/{id}");
        }


        public async Task<Teacher> CreateTeacher(CreateTeacherDto teacher)
        {
            var request = new StringContent(JsonConvert.SerializeObject(teacher), UnicodeEncoding.UTF8, "application/json");
            var responce = await _httpClient.PostAsync("CreateTeacher/", request);
            var result = JsonConvert.DeserializeObject<Teacher>(await responce.Content.ReadAsStringAsync());

            return result;
        }

        public async Task<Teacher> DeleteTeacher(int id)
        {
            return await _httpClient.DeleteFromJsonAsync<Teacher>($"DeleteTeacher/{id}");
        }

        public async Task<Teacher> UpdateTeacheres(CreateTeacherDto teacher)
        {
            var responce = new StringContent(JsonConvert.SerializeObject(teacher), UnicodeEncoding.UTF8, "application/json");
            var resultat = await _httpClient.PutAsync($"UpdateTeacher/", responce);

            return JsonConvert.DeserializeObject<Teacher>(await resultat.Content.ReadAsStringAsync());
        }
    }
}
