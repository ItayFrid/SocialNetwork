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
    [Authorize(Roles = "Student")]
    public class RatingController : Controller
    {
        // GET: Rating
        public ActionResult Index()
        {
            return View();
        }

		public ActionResult RatingRegister()
		{
            DataLayer dal = new DataLayer();
            ViewModel vm = new ViewModel();
            int courseId = int.Parse(Request.Form["inputCourseId"]);
            string studentEmail = User.Identity.Name;
            vm.course = (from x in dal.courses
                         where x.id == courseId
                         select x).ToList<Course>()[0];
            vm.rating = new Rating();
			ViewBag.message = "";
			return View(vm);
		}
        
		public ActionResult addRating(Rating rating)
		{
            //Getting ID from form
            int courseId = int.Parse(Request.Form["inputCourseId"]);
            string studentEmail = User.Identity.Name;
            DataLayer dal = new DataLayer();
			if (ModelState.IsValid)
			{
                //Asign rating course and student from DB
                rating.course = (from x in dal.courses
                                 where x.id == courseId
                                 select x).ToList<Course>()[0];
                rating.student = (from x in dal.students
                                  where x.email == studentEmail
                                  select x).ToList<Student>()[0];
                //Adding the rating to the course ratings list
                rating.course.ratings.Add(rating);
                rating.course.calcAvg();
                dal.ratings.Add(rating);
				dal.SaveChanges();
				ViewBag.message = "Rating was submitted succesfully.";
                return RedirectToAction("ShowCourses", "Student");
            }
			else
				ViewBag.message = "Error in submitting the rating.";
			return View("RatingRegister", rating);
		}
	}
}