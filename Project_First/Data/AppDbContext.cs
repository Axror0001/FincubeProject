using Microsoft.EntityFrameworkCore;
using Project_First.Models;

namespace Project_First.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<TeacherStudent> TeachersStudents { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TeacherStudent>()
            .HasOne(ts => ts.Teacher)
            .WithMany(t => t.TeacherStudents)
            .HasForeignKey(ts => ts.TeacherId);

            modelBuilder.Entity<TeacherStudent>()
            .HasOne(ts => ts.Student)
            .WithMany(s => s.TeacherStudents)
            .HasForeignKey(ts => ts.StudentId);

        }
    }
}
