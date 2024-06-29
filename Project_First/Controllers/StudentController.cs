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
    public class StudentController : ControllerBase
    {
        public readonly AppDbContext _stContext;
        public StudentController(AppDbContext stContext)
        {
            this._stContext = stContext;
        }



        [HttpGet]
        public async Task<IActionResult> GetAllStudent()
        {
            var student = await _stContext.Students.ToListAsync();
            return Ok(student);
        }



        [HttpGet("{StudentID}")]
        public async Task<IActionResult> GetByIdStudent(int StudentID)
        {
            var student = await _stContext.Students.FirstOrDefaultAsync(x => x.studentId.Equals(StudentID));
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }




        [HttpPost]
        public async Task<IActionResult> UploadStudents(IFormFile e)
        {
            /*var files = e.File;*/

            List<Student> students = new List<Student>();
            using (var stream = new MemoryStream())
            {

                await e.CopyToAsync(stream);
                stream.Position = 0;
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;


                    for (int row = 2; row <= rowCount; row++)
                    {
                        var student = new Student
                        {

                            Name = worksheet.Cells[row, 2].Value?.ToString().Trim(),
                            Address = worksheet.Cells[row, 3].Value?.ToString().Trim(),
                            Age = Convert.ToInt32(worksheet.Cells[row, 4].Value?.ToString().Trim()),
                            LastName = worksheet.Cells[row, 5].Value?.ToString().Trim(),
                            Source = Convert.ToInt32(worksheet.Cells[row, 6].Value?.ToString().Trim()),
                            University = worksheet.Cells[row, 7].Value?.ToString().Trim(),
                        };
                        students.Add(student);
                    }
                }
            }
            await _stContext.Students.AddRangeAsync(students);
            await _stContext.SaveChangesAsync();
            return Ok();
        }



        [HttpPost]
        public async Task<IActionResult> CreateStudent(CreateStudentDto studentDTO)
        {
            var student = new Student()
            {
                Name = studentDTO.Name,
            };

            var result = await _stContext.Students.AddAsync(student);

            await _stContext.SaveChangesAsync();
            return Ok(student);
        }



        [HttpPut]
        public async Task<IActionResult> UpdateStudent(CreateStudentDto student)
        {
            var result = await _stContext.Students.FirstOrDefaultAsync(x => x.studentId.Equals(student.studentId));
            if (result == null)
            {
                return NotFound();
            }
            result.Name = student.Name;
            _stContext.Students.Update(result);
            await _stContext.SaveChangesAsync();
            return Ok(result);
        }


        [HttpDelete("{StudentID}")]
        public async Task<IActionResult> DeleteStudent(int StudentID)
        {
            var student = await _stContext.Students.
                FirstOrDefaultAsync(x => x.studentId.Equals(StudentID));
            if (student == null)
            {
                return NotFound();
            }
            _stContext.Students.Remove(student);
            _stContext.SaveChanges();
            return Ok(student);
        }
    }
}
