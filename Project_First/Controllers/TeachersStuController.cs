using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_First.Data;
using Project_First.Models;

namespace Project_First.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TeachersStuController : ControllerBase
    {
        public readonly AppDbContext _dbContext;
        public TeachersStuController(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _dbContext.TeachersStudents.ToListAsync();
            return Ok(result);
        }

        [HttpGet("{TeacherID}")]
        public async Task<IActionResult> GetTeachIdStudents(int TeacherID)
        {
            var students = await _dbContext.TeachersStudents
                .Where(x => x.TeacherId.Equals(TeacherID))
                .Select(p => p.Student)
                .ToListAsync();

            if (students == null)
            {
                return NotFound();
            }
            return Ok(students);

        }
        [HttpGet("{StudentId}")]
        public async Task<IActionResult> GetStIdTeachers(int StudentId)
        {
            var teachers = _dbContext.TeachersStudents
                .Where(x => x.StudentId.Equals(StudentId))
                .Select(x => x.Teacher).ToList();
            if (teachers == null)
            {
                return NotFound();
            }
            return Ok(teachers);
        }


        [HttpPost]
        public async Task<IActionResult> CreateTeachStu(int studentId, int teacherId)
        {
            var result1 = await _dbContext.TeachersStudents.FirstOrDefaultAsync(x => x.StudentId.Equals(studentId));
            var result2 = await _dbContext.TeachersStudents.FirstOrDefaultAsync(x => x.TeacherId.Equals(teacherId));
            if (result1 != null || result2 != null)
            {
                return BadRequest();
            }
            else
            {

                var teacherAndStudents = await _dbContext.TeachersStudents
                .AddAsync(new TeacherStudent { StudentId = studentId, TeacherId = teacherId });

                await _dbContext.SaveChangesAsync();

                var result = await _dbContext.TeachersStudents
                    .Include(p => p.Teacher)
                    .Include(p => p.Student)
                    .FirstOrDefaultAsync(p => p.Id == teacherAndStudents.Entity.Id);


                return Ok(result);

            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteTeachIdStu([FromQuery] int stId, [FromQuery] int teachId)
        {
            var teachers = await _dbContext.TeachersStudents.FirstOrDefaultAsync(x => x.TeacherId == teachId && x.StudentId == stId);
            _dbContext.TeachersStudents.Remove(teachers);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
