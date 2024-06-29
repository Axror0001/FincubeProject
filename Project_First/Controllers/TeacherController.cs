using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using Project_First.Data;
using Project_First.Dto;
using Project_First.Models;

namespace Project_First.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        public readonly AppDbContext _dbContext;
        public TeacherController(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllTeacher()
        {
            var teacher = await _dbContext.Teachers.ToListAsync();
            return Ok(teacher);
        }



        [HttpGet("{TeacherID}")]
        public async Task<IActionResult> GetByIdTeacher(int TeacherID)
        {
            var teacher = await _dbContext.Teachers.
                FirstOrDefaultAsync(x => x.teacherId.Equals(TeacherID));

            if (teacher == null)
            {
                return NotFound();
            }
            return Ok(teacher);

        }




        [HttpPost]
        public async Task<IActionResult> CreateTeacher(CreateTeacherDto teacher)
        {
            var teachers = new Teacher()
            {
                Name = teacher.Name,
                Science = teacher.Science,
            };
            var teachera = await _dbContext.Teachers.AddAsync(teachers);
            await _dbContext.SaveChangesAsync();

            return Ok(teachers);
        }



        [HttpPost]
        public async Task<IActionResult> UploadTeacher(IFormFile e)
        {
            try
            {
                List<Teacher> teachers = new List<Teacher>();
                List<string> Columns = new List<string>();
                List<List<string>> Rows = new List<List<string>>();
                //var file = e.File;
                using (var stream = new MemoryStream())
                {
                    await e.OpenReadStream().CopyToAsync(stream);
                    stream.Position = 0;
                    ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                    using (var package = new ExcelPackage(stream))
                    {
                        var worksheet = package.Workbook.Worksheets[0];
                        var rowCount = worksheet.Dimension.Rows;
                        var colCount = worksheet.Dimension.Columns;

                        Columns.Clear();
                        Rows.Clear();

                        for (int row = 2; row <= rowCount; row++)
                        {
                            var teacher = new Teacher()
                            {

                                Name = worksheet.Cells[row, 2].Value?.ToString().Trim(),
                                Science = worksheet.Cells[row, 3].Value?.ToString().Trim(),
                                Address = worksheet.Cells[row, 4].Value?.ToString().Trim(),
                                Age = Convert.ToInt32(worksheet.Cells[row, 5].Value?.ToString().Trim()),
                                LastName = worksheet.Cells[row, 6].Value?.ToString().Trim(),
                                Lavel = worksheet.Cells[row, 7].Value?.ToString().Trim(),
                                Phone_Number = Convert.ToInt32(worksheet.Cells[row, 8].Value?.ToString().Trim()),

                            };
                            teachers.Add(teacher);
                        }
                    }
                }
                await _dbContext.Teachers.AddRangeAsync(teachers);
                await _dbContext.SaveChangesAsync();

                return Ok(e);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        [HttpPut]
        public async Task<IActionResult> UpdateTeacher(CreateTeacherDto teacher)
        {
            var teachers = await _dbContext.Teachers.FirstOrDefaultAsync(x => x.teacherId.Equals(teacher.teacherId));
            if (teachers == null)
            {
                return NotFound();
            }
            teachers.Name = teacher.Name;
            teachers.Science = teacher.Science;

            _dbContext.Teachers.Update(teachers);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }



        [HttpDelete("{TeacherID}")]
        public async Task<IActionResult> DeleteTeacher(int TeacherID)
        {
            var teacher = await _dbContext.Teachers.
                FirstOrDefaultAsync(x => x.teacherId.Equals(TeacherID));
            if (teacher == null)
            {
                return NotFound();
            }
            _dbContext.Teachers.Remove(teacher);
            _dbContext.SaveChanges();
            return Ok(teacher);
        }
    }
}
