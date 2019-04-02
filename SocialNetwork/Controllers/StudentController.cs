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
            complaint.Sender = User.Identity.Name;
            complaint.Target = Request.Form["complaint.Target"];
            dal.complaints.Add(complaint);
            dal.SaveChanges();
            return RedirectToAction("Student","Student");
        }
    }
}