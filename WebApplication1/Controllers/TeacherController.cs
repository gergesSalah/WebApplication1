using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.DTO;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class TeacherController : Controller
    {
        private SchoolContext SchoolContext = new SchoolContext();
        public IActionResult Index()
        {
            var teachers = SchoolContext.Teachers.Include(t=>t.Students);
            return View(teachers);
        }

        [HttpGet]
        public IActionResult GetStudentsOf(int TeacherId)
        {
            try
            {
                var students = SchoolContext.Students.Where(s => s.TeacherId == TeacherId).ToList();
                return Json(new Response<List<Student>> { Message= "Success",Status = 200 , Data = students});
            }
            catch (Exception ex) 
            {
                return Json(new Response<List<Student>> { Message = "failed", Status = 400, ErrorMessage = ex.Message });

            }
        }
    }

}
