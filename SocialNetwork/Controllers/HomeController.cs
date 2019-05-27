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
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult OurTeachers()
        {
            DataLayer dal = new DataLayer();
            ViewModel vm = new ViewModel();
            vm.teachers = dal.teachers.ToList<Teacher>();
            return View(vm);
        }
        
    }
}