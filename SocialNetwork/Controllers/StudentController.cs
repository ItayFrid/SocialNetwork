using Newtonsoft.Json;
using SocialNetwork.DAL;
using SocialNetwork.Models;
using SocialNetwork.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SocialNetwork.Controllers
{
    [Authorize(Roles ="Student")]
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Student()
        {
            DataLayer dal = new DataLayer();
            List<Student> student = (from x in dal.students
                                     where x.email == User.Identity.Name
                                     select x).ToList<Student>();       //Getting student from db
            return View(student[0]);
        }

        public ActionResult Complaint()
        {
            ViewModel vm = new ViewModel();
            DataLayer dal = new DataLayer();
            vm.teachers = (from x in dal.teachers
                           where x.role == "Teacher"
                           select x).ToList<Teacher>();
            vm.student = (from x in dal.students
                          where x.email == User.Identity.Name
                          select x).ToList<Student>()[0];
            Complaint complaint = new Complaint();
            return View(vm);
        }

        public ActionResult NewComplaint(Complaint complaint)
        {
            DataLayer dal = new DataLayer();
            User sender = (from x in dal.users
                           where x.email == User.Identity.Name
                           select x).ToList<User>()[0];
            complaint.Sender = sender;
            string targetEmail = Request.Form["complaint.Target"];
            User target = (from x in dal.users
                           where x.email == targetEmail
                           select x).ToList<User>()[0];
            complaint.Target = target;
            dal.complaints.Add(complaint);
            dal.SaveChanges();
            return RedirectToAction("Student","Student");
        }

        public ActionResult ShowCourses()
        {
            DataLayer dal = new DataLayer();
            ViewModel vm = new ViewModel();
            vm.courses = (from x in dal.courses
                          select x).ToList<Course>();
            vm.student = (from x in dal.students
                          where x.email == User.Identity.Name
                          select x).ToList<Student>()[0];            
            return View(vm);
        }
        public ActionResult EnrollCourse()
        {
            DataLayer dal = new DataLayer();
            int id = int.Parse(Request.Form["enroll"]);
            Student student = (from x in dal.students
                               where x.email == User.Identity.Name
                               select x).ToList<Student>()[0];
            Course course = (from x in dal.courses
                             where x.id == id
                             select x).ToList<Course>()[0];
            course.students.Add(student);
            student.courses.Add(course);
            Progress studentProg = new Progress();
            studentProg.student = student;
            studentProg.course = course;
            studentProg.prog = 0;
            course.studentProgress.Add(studentProg);
            dal.SaveChanges();
            return RedirectToAction("ShowCourses", "Student");
        }

        public ActionResult Search()
        {
            return View();
        }
        
        public ActionResult ReferenceNewStudent()
        {
            return View();
        }

        public ActionResult SendReference()
        {
            string email = Request.Form["email"];
            string sender = Request.Form["sender"];
            string message = Request.Form["message"];
            var Url = "/Login/UserRegister/";
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, Url);
            string subject = "Registration Invitation - TeachMe";
            string body = "<p>" +
                "You are invited to join TeachMe by <b>" +
                sender +
                "</b><br/>please click this" +
                "<a href='" + link + "'>link</a> to join us. <br/>" +
                message +
                "</p>";
            SendEmail(email, subject, body);
            return RedirectToAction("Student","Student");
        }

        public ActionResult Course(int id)
        {
            DataLayer dal = new DataLayer();
            ViewModel vm = new ViewModel();
            vm.course = (from x in dal.courses
                             where x.id == id
                             select x).ToList<Course>()[0];
            vm.student = (from x in dal.students
                          where x.email == User.Identity.Name
                          select x).ToList<Student>()[0];
            return View(vm);
        }

        public JsonResult getTeachersJson()
        {
            DataLayer dal = new DataLayer();
            var teachers = (from x in dal.teachers
                            select new {email = x.email,name = x.name });
            return Json(teachers, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getCoursesJson()
        {
            DataLayer dal = new DataLayer();
            var courses = (from x in dal.courses
                           select new {id = x.id, name = x.name, teacher=x.teacher.name });
            return Json(courses, JsonRequestBehavior.AllowGet);
        }

        private void SendEmail(string toEmail, string subject, string body)
        {
            string sender = "immunit7@gmail.com";
            string password = "fitay123";
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Timeout = 10000,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(sender, password)
            };
            MailMessage msg = new MailMessage(sender, toEmail, subject, body)
            {
                IsBodyHtml = true,
                BodyEncoding = UTF8Encoding.UTF8
            };
            client.Send(msg);
        }
    }
}