using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialNetwork.Models;
using SocialNetwork.DAL;
using SocialNetwork.Classes;
using System.Web.Security;
using SocialNetwork.ViewModels;

namespace SocialNetwork.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AdminLogin()
        {

            return View();
        }
        [AllowAnonymous]
        public ActionResult AdminRegister()
        {
            return View();
        }

        public ActionResult Admin()
        {
            DataLayer dal = new DataLayer();
            ViewModel vm = new ViewModel();
            vm.teachers = (from x in dal.teachers
                                      where x.authorized == false
                                      select x).ToList<Teacher>();       //Attempting to get unauthorized teachers from database
            vm.complaints = (from x in dal.complaints
                             select x).ToList<Complaint>();
            return View("Admin", vm);
        }

        //This 
        public ActionResult SetAuth()
        {
            DataLayer dal = new DataLayer();
     
            string email = Request.Form["auth"].ToString();
            foreach (Teacher t in dal.teachers)
            {
                if (t.email.Equals(email))
                    t.authorized = true;
            }
            dal.SaveChanges();

            return RedirectToAction("Admin");
        }


        public ActionResult Login(User user)
        {

            DataLayer dal = new DataLayer();
            Encryption enc = new Encryption();
            List<User> userToCheck = (from x in dal.users
                                      where x.email == user.email
                                      select x).ToList<User>();       //Attempting to get user information from database
            if (userToCheck.Count != 0)     //In case username was found
            {
                if (enc.ValidatePassword(user.password, userToCheck[0].password))   //Correct password
                {
                    var authTicket = new FormsAuthenticationTicket(
                        1,                                  // version
                        user.email,                      // user name
                        DateTime.Now,                       // created
                        DateTime.Now.AddMinutes(20),        // expires
                        true,       //keep me connected
                        userToCheck[0].role                       // store roles
                        );

                    string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

                    var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    Response.Cookies.Add(authCookie);
                    return View("Admin");
                    
                }
                else
                {
                    ViewBag.UserLoginMessage = "Incorrect Username/password";
                }
            }
            else
                ViewBag.UserLoginMessage = "Incorrect Username/password";
            return View("AdminLogin", user);
        }
        public ActionResult BlockUser()
        {
            DataLayer dal = new DataLayer();

            string email = Request.Form["block"].ToString();
            foreach (User u in dal.users)
            {
                if (u.email.Equals(email))
                    u.blocked = !u.blocked;
            }
            dal.SaveChanges();

            return RedirectToAction("ManageUsers");
        }

        public ActionResult ManageUsers()
        {
            ViewModel vm = new ViewModel();
            DataLayer dal = new DataLayer();
            vm.users = (from x in dal.users
                        select x).ToList<User>();
            List<User> usr = (from x in dal.users
                              where x.email == User.Identity.Name
                              select x ).ToList<User>();
            vm.user = usr[0];
            return View(vm);
        }

        public ActionResult ShowUsers()
        {
            ViewModel vm = new ViewModel();
            DataLayer dal = new DataLayer();
            vm.teachers = (from x in dal.teachers
                        select x).ToList<Teacher>();
            vm.students = (from x in dal.students
                           select x).ToList<Student>();
            vm.users = (from x in dal.users
                        where x.role == "Admin"
                        select x).ToList<User>();
            return View(vm);
        }

        public ActionResult AdminComplaints()
        {
            ViewModel vm = new ViewModel();
            DataLayer dal = new DataLayer();
            vm.complaints = (from x in dal.complaints
                             select x).ToList<Complaint>();
            return View(vm);
        }

        public ActionResult DeleteComplaint()
        {
            DataLayer dal = new DataLayer();
            int id = int.Parse(Request.Form["delete"]);
            Complaint complaint = (from x in dal.complaints
                                   where x.id == id
                                   select x).ToList<Complaint>()[0];
            dal.complaints.Remove(complaint);
            dal.SaveChanges();
            return RedirectToAction("AdminComplaints", "Admin");
        }
        
        public ActionResult ViewStatistics()
        {
            int studentEnrolls = 0;
            DataLayer dal = new DataLayer();
            ViewModel vm = new ViewModel();
            vm.teachers = (from x in dal.teachers
                        select x).ToList<Teacher>();
            vm.students = (from x in dal.students
                           select x).ToList<Student>();
            vm.users = (from x in dal.users
                           select x).ToList<User>();
            vm.courses = (from x in dal.courses
                          select x).ToList<Course>();
            vm.complaints = (from x in dal.complaints
                             select x).ToList<Complaint>();
            vm.messages = (from x in dal.messages
                             select x).ToList<Message>();
            foreach (Course course in vm.courses)
                studentEnrolls += course.students.Count;
            ViewBag.enrolls = studentEnrolls;
            return View(vm);
        }

        public ActionResult SendGlobalMessage()
        {
            ViewModel vm = new ViewModel();
            vm.message = new Message();
            ViewBag.message = "";
            return View(vm);
        }

        public ActionResult AddGlobalMessage(Message message)
        {
            DataLayer dal = new DataLayer();
            User admin = (from x in dal.users
                         where x.email == User.Identity.Name
                         select x).ToList<User>()[0];
            List<User> users = (from x in dal.users
                                where x.email != User.Identity.Name
                                select x).ToList<User>();
            if(users.Count==0)
                return RedirectToAction("SendGlobalMessage", "Admin");
            foreach(User user in users)
            {
                Message tmpMessage = new Message { to=user,from = admin,body = message.body };
                dal.messages.Add(tmpMessage);
            }
            dal.SaveChanges();
            return RedirectToAction("Admin", "Admin");
        }
    }
}