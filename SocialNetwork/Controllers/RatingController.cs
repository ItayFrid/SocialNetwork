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

		public ActionResult ratingRegister()
		{
			Rating rating = new Rating();
			ViewBag.message = "";
			return View(rating);
		}

		public ActionResult addRating(Rating rating)
		{
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
			return View("ratingRegister", rating);
		}
	}
}