using SocialNetwork.DAL;
using SocialNetwork.Models;
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
            Complaint complaint = new Complaint();
            return View(complaint);
        }
    }
}