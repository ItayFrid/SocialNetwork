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
    [Authorize]
    public class PostController : Controller
    {
        // GET: Post
        public ActionResult Index()
        {
            DataLayer dal = new DataLayer();
            ViewModel vm = new ViewModel();
            vm.posts = (from x in dal.posts
                        select x).ToList<Post>();
            vm.posts = vm.posts.OrderByDescending(p => p.isPromoted).ToList<Post>();
            return View(vm);
        }
        
        public ActionResult WritePost()
        {
            Post post = new Post();
            return View(post);
        }

        public ActionResult AddPost(Post post)
        {
            DataLayer dal = new DataLayer();
            string userEmail = User.Identity.Name;
            post.user = (from x in dal.users
                         where x.email == userEmail
                         select x).ToList<User>()[0];
            post.date = DateTime.Now;
            if (User.IsInRole("Teacher"))
            {
                Teacher teacher = (Teacher)post.user;
                post.isPromoted = teacher.isPromoted;
            }
            else
                post.isPromoted = false;
            if (User.IsInRole("Admin"))
            {
                string studySource = Request.Form["studySource"];
                if (studySource == "True")
                    post.isStudySource = true;
                else
                    post.isStudySource = false;
            }
            else
                post.isStudySource = false;
            dal.posts.Add(post);
            dal.SaveChanges();
            return RedirectToAction("Index", "Post");
        }
    }
}