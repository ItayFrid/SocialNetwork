using System;
using SocialNetwork.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SocialNetwork.DAL;
using SocialNetwork.Classes;


//Test
namespace SocialNetwork.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        /*This function redirects to user login page*/
        public ActionResult UserLogin()
        {
            User user = new User();
            ViewBag.UserLoginMessage = "";
            return View(user);
        }

        /*This function handles user login*/
        /*Given information from user login form*/
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

                    if(userToCheck[0].role.Equals("Admin"))
                        return RedirectToAction("Admin", "Admin");
                    else if(userToCheck[0].role.Equals("Teacher"))
                        return RedirectToAction("Teacher", "Teacher");
                    else
                        return RedirectToAction("Student", "Student");
                }
                else
                {
                    ViewBag.UserLoginMessage = "Incorrect Username/password";
                }
            }
            else
                ViewBag.UserLoginMessage = "Incorrect Username/password";
            return View("UserLogin", user);
        }

        /*This function handles signing out*/
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToRoute("HomePage");
        }

        /*This function redirects to user register page*/
        public ActionResult UserRegister()
        {
            User user = new User();
            ViewBag.UserLoginError = "";
            return View(user);
        }

        /*This function adds new user to database*/
        /*Given user information from user register form*/
        public ActionResult AddUser(User user)
        {
            DataLayer dal = new DataLayer();
            Encryption enc = new Encryption();
            if(dal.users.ToList<User>().Count == 0)
            {
                User root = new User
                {
                    id = "111111111",
                    email = "admin@gmail.com",
                    password = enc.CreateHash("123"),
                    name = "admin",
                    role = "Admin",
                    blocked = false
                };
                dal.users.Add(root);
            }
            if (ModelState.IsValid)
            {
                string hashedPassword = enc.CreateHash(user.password);      //Encrypting user's password
                if (!userExists(user.email))     //Adding user to database
                {
                    user.password = hashedPassword;
                    
                    if (user.role.Equals("Student")) {
                        //Role: Student
                        Student student = createStudent(user);
						dal.students.Add(student);
                        dal.SaveChanges();
                    }
                    else if(user.role.Equals("Teacher"))
                    {
                        //Role: Teacher
                        Teacher teacher = createTeacher(user);
						dal.teachers.Add(teacher);
                        dal.SaveChanges();
                    }
                    else
                    {
                        dal.users.Add(user);
                        dal.SaveChanges();
                    }
                    
                    ViewBag.message = "User was added succesfully.";
                    return View("UserLogin",user);
                }
                else
                    ViewBag.message = "Email Exists in database.";
            }
            else
                ViewBag.message = "Error in registration.";
            user = new User();
            return View("UserRegister", user);
        }

        /*This function compares given username with usernames in database*/
        public bool userExists(string userName)
        {
            DataLayer dal = new DataLayer();
            List<User> users = dal.users.ToList<User>();
            foreach (User user in dal.users)
                if (user.email.Equals(userName))
                    return true;
            return false;
        }

		/**
		 * this function Creates a student from user 
		 **/
		private Student createStudent(User user)
		{
            Student student = new Student
            {
                email = user.email,
                password = user.password,
                name = user.name,
                role = user.role,
                ratings = "",
                id = user.id
			};
			return student;
		}

		/**
		 * this function Creates a teacher from user 
		 **/
		private Teacher createTeacher(User user)
		{
            Teacher teacher = new Teacher
            {
                email = user.email,
                password = user.password,
                name = user.name,
                role = user.role,
                Rating = 0,
                numRating = 0,
                id = user.id,
                authorized = false,
                resume = ""

			};
			return teacher;
		}
	}
}