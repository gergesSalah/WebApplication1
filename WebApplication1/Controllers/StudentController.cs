using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApplication1.Data;
using WebApplication1.DTO;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class StudentController : Controller
    {
        //unused
        private SchoolContext SchoolContext =new SchoolContext();
        public IActionResult Index()
        {
            //List<Student> students = SchoolContext.Students.ToList();
            //var f = SchoolContext.Students.FromSqlRaw("select students.Id,students.Name,students.Email,students.TeL ,students.TeacherId,Teacher.Name Teacher from students join Teacher on (students.TeacherId = Teacher.ID)").ToList();
            List<Student> students = SchoolContext.Students.Include(std => std.Teacher).ToList();
            
            return View(students);
        }

        public IActionResult Index2()
        {
            //List<Student> students = SchoolContext.Students.ToList();
            List<Student> students = SchoolContext.Students.Include(std => std.Teacher).ToList();

            return View(students);
        }



        [HttpGet]
        public IActionResult Create()
        {

            return PartialView("_CreateNewStudent",SchoolContext.Teachers.ToList());
        }

        [HttpPost]
        public IActionResult Create(Student student)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return Json(new Response<Student> { Data = student, ErrorMessage = "model is not vaild" , Message = "failed", Status = 400 });

                }
                if(SchoolContext.Students.Any(stud => stud.TeL == student.TeL))
                {
                    return Json(new Response<Student> { Data = student, ErrorMessage = "student is already existing", Message = "student is already existing", Status = 400 });

                }
                //SchoolContext.Database.OpenConnection();
                //SchoolContext.Students.FromSqlRaw("SET IDENTITY_INSERT dbo.Students ON");
                //Teacher teach = new Teacher();

                //student.Teacher = SchoolContext.Teachers.Where(tech => tech.Id == student.TeacherId).FirstOrDefault();
                SchoolContext.Students.Add(student);
                SchoolContext.SaveChanges();
                return Json(new Response<Student> { Data = student, Message = "The student added successfully" ,Status = 200});

            }
            catch (Exception ex)
            {
                return Json(new Response<Student> { Data = student, ErrorMessage = ex.Message, Message = "failed", Status = 400 });

            }
            //return RedirectToAction("Create");
        }



        [HttpGet]
        public IActionResult Update(int StudentId)
        {
            Student student = SchoolContext.Students.Find(StudentId);
            ViewBag.Student = student;
            return PartialView("_UpdateStudent", SchoolContext.Teachers.ToList());
        }

        [HttpPost]
        public IActionResult Update(Student student)
        {
            try
            {   
                if(student == null)
                    return Json(new Response<Student> { Status = 404, Data = student, Message = "failed", ErrorMessage = "null value" });
                if (!SchoolContext.Students.Any(s=>s.Id==student.Id))
                {
                    return Json(new Response<Student> { Data = student, ErrorMessage = "student is not existing", Message = "failed", Status = 404 });

                }
                var jf = SchoolContext.Students.Update(student);
                SchoolContext.SaveChanges();
                return Json(new Response<Student> { Message = "updated successfully", Status = 200, Data = student });

            }
            catch
            {
                return Json(new Response<Student> { Message = "update failed", Status = 400, Data = student });
            }

        }

        [HttpPost]
        public IActionResult Delete(int studentId)
        {
            Student student = SchoolContext.Students.Find(studentId);
            try
            {
                if(student == null)
                    return Json(new Response<Student> { Status = 404, Data = student, Message = "failed", ErrorMessage = "not found" });

                SchoolContext.Students.Remove(student);
                SchoolContext.SaveChanges();
                return Json(new Response<Student> {  Status = 200, Data = student,Message = "deleted successfully"});
            }
            catch(Exception ex) 
            {
                return Json(new Response<Student> { Status = 400, Data = student, Message = "failed",ErrorMessage = ex.Message });

            }
        }


    }
}
