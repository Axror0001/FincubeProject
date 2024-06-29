using System.ComponentModel.DataAnnotations;

namespace Project_First.Models
{
    public class TeacherStudent
    {
        [Key]

        public int Id { get; set; }


        public int StudentId { get; set; }
        public Student? Student { get; set; }


        public int TeacherId { get; set; }
        public Teacher? Teacher { get; set; }
    }
}
