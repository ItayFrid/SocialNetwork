using SocialNetwork.DAL;
using SocialNetwork.Models;
using SocialNetwork.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            dal.SaveChanges();
            return RedirectToAction("ShowCourses", "Student");
        }
    }
}