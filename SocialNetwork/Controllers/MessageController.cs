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
    public class MessageController : Controller
    {
        // GET: Message
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SendMessage()
        {
            DataLayer dal = new DataLayer();
            ViewModel vm = new ViewModel();
            vm.message = new Message();
            vm.users = (from x in dal.users
                        select x).ToList<User>();
            User user = (from x in dal.users
                         where x.email == User.Identity.Name
                         select x).ToList<User>()[0];
            vm.users.Remove(user);
            ViewBag.message = "";
            return View(vm);
        }

        public ActionResult AddMessage(Message message)
        {
            DataLayer dal = new DataLayer();
            User from = (from x in dal.users
                           where x.email == User.Identity.Name
                           select x).ToList<User>()[0];
            message.from = from;
            string toEmail = Request.Form["message.to"];
            User to = (from x in dal.users
                           where x.email == toEmail
                           select x).ToList<User>()[0];
            message.to = to;
            dal.messages.Add(message);
            dal.SaveChanges();
            return RedirectToAction("ViewMessages", "Message");
        }

        public ActionResult ViewMessages()
        {
            DataLayer dal = new DataLayer();
            ViewModel vm = new ViewModel();
            vm.messages = (from x in dal.messages
                           where x.to.email == User.Identity.Name
                           select x).ToList<Message>();
            return View(vm);
        }

        public ActionResult DeleteMessage()
        {
            DataLayer dal = new DataLayer();
            int id = int.Parse(Request.Form["delete"]);
            Message message = (from x in dal.messages
                                   where x.id == id
                                   select x).ToList<Message>()[0];
            dal.messages.Remove(message);
            dal.SaveChanges();
            return RedirectToAction("ViewMessages", "Message");
        }

        public JsonResult getMessagesJson()
        {
            DataLayer dal = new DataLayer();
            var messages = (from x in dal.messages
                            where x.to.email == User.Identity.Name
                            select new { to = x.to.email,body = x.body});
            return Json(messages, JsonRequestBehavior.AllowGet);
        }
    }
}