using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialNetwork.DAL;
using SocialNetwork.Models;

namespace SocialNetwork.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }


        //This action creates and updates new course
        public ActionResult addCourse(Course newCourse)
        {
            DataLayer dal = new DataLayer();


            List<Teacher> teacher = (from x in dal.teachers
                                      where x.email == User.Identity.Name
                                      select x).ToList<Teacher>();       //Getting teacher from db

            newCourse.ratings = null;
            newCourse.avgRating = 0;
            newCourse.teacher = teacher[0];
            teacher[0].courses.Add(newCourse);
            dal.courses.Add(newCourse);

            dal.SaveChanges();
            

            return View("Teacher");
        }

        
    }
}