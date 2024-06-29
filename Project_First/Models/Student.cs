using System.ComponentModel.DataAnnotations;

namespace Project_First.Models
{
    public class Student
    {
        [Key]
        public int studentId { get; set; }
        public int Age { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string University { get; set; }
        public int Source { get; set; }
        public List<TeacherStudent>? TeacherStudents { get; set; }
    }
}
