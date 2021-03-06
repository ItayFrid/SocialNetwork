﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialNetwork.DAL;
using SocialNetwork.Models;
using SocialNetwork.ViewModels;

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
            newCourse.AssistantEmail = "";
            newCourse.AssistantAccept = false;
            //Updating database
            teacher[0].courses.Add(newCourse);
            dal.courses.Add(newCourse);
            dal.SaveChanges();

            return View("Teacher", teacher[0]);
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
            teacher[0].resume = Request.Form["Resume"];
            dal.SaveChanges();
            return View("Teacher", teacher[0]);
        }

        public ActionResult ViewCourses()
        {
            DataLayer dal = new DataLayer();
            ViewModel vm = new ViewModel();
            vm.teacher = (from x in dal.teachers
                          where x.email == User.Identity.Name
                          select x).ToList<Teacher>()[0];
            vm.courses = (from x in dal.courses
                          where x.teacher.email == vm.teacher.email
                          select x).ToList<Course>();
            return View(vm);
        }

        public ActionResult EditBooks()
        {
            DataLayer dal = new DataLayer();
            ViewModel vm = new ViewModel();
            int id = int.Parse(Request.Form["courseID"]);
            vm.course = (from x in dal.courses
                         where x.id == id
                         select x).ToList<Course>()[0];
            return View(vm);
        }

        public ActionResult Books()
        {
            DataLayer dal = new DataLayer();
            int id = int.Parse(Request.Form["id"]);
            Course course = (from x in dal.courses
                             where x.id == id
                             select x).ToList<Course>()[0];
            course.books = Request.Form["Books"];
            dal.SaveChanges();
            return RedirectToAction("ViewCourses", "Teacher");
        }

        public ActionResult Complaint()
        {
            ViewModel vm = new ViewModel();
            DataLayer dal = new DataLayer();
            vm.students = (from x in dal.students
                           where x.role == "Student"
                           select x).ToList<Student>();
            vm.teacher = (from x in dal.teachers
                          where x.email == User.Identity.Name
                          select x).ToList<Teacher>()[0];
            Complaint complaint = new Complaint();
            return View(vm);
        }

        public ActionResult NewComplaint(Complaint complaint)
        {
            DataLayer dal = new DataLayer();
            User sender = (from x in dal.users
                           where x.email == User.Identity.Name
                           select x).ToList<User>()[0];
            complaint.Sender = sender;
            string targetEmail = Request.Form["complaint.Target"];
            User target = (from x in dal.users
                           where x.email == targetEmail
                           select x).ToList<User>()[0];
            complaint.Target = target;
            dal.complaints.Add(complaint);
            dal.SaveChanges();
            return RedirectToAction("Teacher", "Teacher");
        }

        public ActionResult GiveCourse()
        {
            DataLayer dal = new DataLayer();
            ViewModel vm = new ViewModel();
            vm.teachers = (from x in dal.teachers
                           where x.email != User.Identity.Name
                           select x).ToList<Teacher>();
            vm.courses = (from x in dal.courses
                          where x.teacher.email == User.Identity.Name
                          select x).ToList<Course>();
            return View(vm);
        }

        public ActionResult ChangeCourseTeacher()
        {
            DataLayer dal = new DataLayer();
            string teacherEmail = Request.Form["teacher.email"];
            int courseId = Int32.Parse(Request.Form["course.id"]);
            Teacher teacherGive = (from x in dal.teachers
                               where x.email == teacherEmail
                               select x).ToList<Teacher>()[0];
            Teacher teacher = (from x in dal.teachers
                               where x.email == User.Identity.Name
                               select x).ToList<Teacher>()[0];
            Course course = (from x in dal.courses
                             where x.id == courseId
                             select x).ToList<Course>()[0];

            course.teacher = teacherGive;
            teacher.courses.Remove(course);
            teacherGive.courses.Add(course);
            dal.SaveChanges();
            return RedirectToAction("Teacher", "Teacher");
        }

        public ActionResult UpdateStudentProgress()
        {
            DataLayer dal = new DataLayer();
            ViewModel vm = new ViewModel();
            string studentEmail = Request.Form["studentEmail"];
            int courseId = Int32.Parse(Request.Form["courseId"]);
            vm.course = (from x in dal.courses
                         where x.id == courseId
                         select x).ToList<Course>()[0];
            foreach (Progress progress in vm.course.studentProgress)
                if (progress.student.email == studentEmail)
                    vm.progress = progress;
            return View(vm);
        }

        public ActionResult SaveStudentProgress()
        {
            int progressId = Int32.Parse(Request.Form["progressId"]);
            int value = Int32.Parse(Request.Form["studentProgress"]);
            DataLayer dal = new DataLayer();
            Progress progress = (from x in dal.progresses
                                 where x.id == progressId
                                 select x).ToList<Progress>()[0];
            progress.prog = value;
            dal.SaveChanges();
            return RedirectToAction("ViewCourses", "Teacher");
        }

        public ActionResult TeacherRating()
        {
            DataLayer dal = new DataLayer();
            ViewModel vm = new ViewModel();
            vm.teachers = (from x in dal.teachers
                           where x.email != User.Identity.Name
                           select x).ToList<Teacher>();
            return View(vm);
        }

        public ActionResult UpdateTeacherRating() {
            string teacherEmail = Request.Form["teacher"];
            int value = Int32.Parse(Request.Form["teacherRating"]);
            DataLayer dal = new DataLayer();
            Teacher teacher = (from x in dal.teachers
                               where x.email == teacherEmail
                               select x).ToList<Teacher>()[0];
            teacher.Rating += value;
            teacher.numRating++;
            dal.SaveChanges();
            return RedirectToAction("Teacher", "Teacher");
        }

        public ActionResult ApproveAssistant(int id)
        {
            DataLayer dal = new DataLayer();
            Course course = (from x in dal.courses
                             where x.id == id
                             select x).ToList<Course>()[0];
            course.AssistantAccept = true;
            dal.SaveChanges();
            return RedirectToAction("ViewCourses", "Teacher");
        }

        public ActionResult ChangeMobility()
        {
            string teacherEmail = User.Identity.Name;
            DataLayer dal = new DataLayer();
            Teacher teacher = (from x in dal.teachers
                               where x.email == teacherEmail
                               select x).ToList<Teacher>()[0];
            teacher.mobile = !teacher.mobile;
            dal.SaveChanges();
            return RedirectToAction("Teacher","Teacher");
        }
    }
}