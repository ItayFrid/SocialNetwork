using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialNetwork.DAL;
using SocialNetwork.Models;

namespace SocialNetwork.Controllers
{
    [Authorize(Roles ="Teacher")]
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Teacher()
        {
            DataLayer dal = new DataLayer();
            List<Teacher> teacher = (from x in dal.teachers
                                     where x.email == User.Identity.Name
                                     select x).ToList<Teacher>();       //Getting teacher from db
            return View(teacher[0]);
        }

        public ActionResult AddCourse()
        {
            Course course = new Course();
            return View(course);
        }

        //This action creates and updates new course
        public ActionResult NewCourse(Course newCourse)
        {
            DataLayer dal = new DataLayer();

            List<Teacher> teacher = (from x in dal.teachers
                                      where x.email == User.Identity.Name
                                      select x).ToList<Teacher>();       //Getting teacher from db

            //Intializing fields not included in form
            newCourse.books = null;
            newCourse.ratings = null;
            newCourse.avgRating = 0;
            newCourse.teacher = teacher[0];

            //Updating database
            teacher[0].courses.Add(newCourse);
            dal.courses.Add(newCourse);
            dal.SaveChanges();

            return View("Teacher");
        }

        public ActionResult EditResume()
        {
            DataLayer dal = new DataLayer();
            List<Teacher> teacher = (from x in dal.teachers
                                     where x.email == User.Identity.Name
                                     select x).ToList<Teacher>();
            return View(teacher[0]);
        }
        public ActionResult Resume()
        {
            DataLayer dal = new DataLayer();
            List<Teacher> teacher = (from x in dal.teachers
                                     where x.email == User.Identity.Name
                                     select x).ToList<Teacher>();

            //teacher[0].resume = Request.Form[]
            return View();
        }

        
    }
}