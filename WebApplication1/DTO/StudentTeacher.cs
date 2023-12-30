using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Models;

namespace WebApplication1.DTO
{
    public class StudentTeacher
    {

        public int Id { get; set; }

        public string? TeL { get; set; }

        public string? Email { get; set; }

        public int? TeacherId { get; set; }

        public string? Name { get; set; }

        public string TeacherName { get; set; }
    }
}
