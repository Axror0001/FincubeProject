using System.ComponentModel.DataAnnotations;

namespace Project_First.Models
{
    public class Teacher
    {
        [Key]
        public int teacherId { get; set; }
        public int Age { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Science { get; set; }
        public string Lavel { get; set; }
        public string Address { get; set; }
        public int Phone_Number { get; set; }
        public List<TeacherStudent> TeacherStudents { get; set; }
    }
}
