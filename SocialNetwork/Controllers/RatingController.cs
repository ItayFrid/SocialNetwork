using SocialNetwork.DAL;
using SocialNetwork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialNetwork.Controllers
{
    public class RatingController : Controller
    {
        // GET: Rating
        public ActionResult Index()
        {
            return View();
        }

		public ActionResult RatingRegister()
		{
            Rating rating = new Rating();
			ViewBag.message = "";
			return View(rating);
		}

		public ActionResult AddRating(Rating rating)
		{
            //rating.id = int.Parse(Request.Form["inputId"]);
            //rating.courseId = int.Parse(Request.Form["inputCourseId"]);
            //rating.rating = int.Parse(Request.Form["inputRating"]);
            DataLayer dal = new DataLayer();
			if (ModelState.IsValid)
			{
				dal.ratings.Add(rating);
				dal.SaveChanges();
				ViewBag.message = "Rating was submitted succesfully.";
				rating = new Rating();
			}
			else
				ViewBag.message = "Error in submitting the rating.";
			return View("RatingRegister", rating);
		}
	}
}