using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetwork.Models
{
    public class Student : User
    {
        public Student()
        {
            courses = new List<Course>();
        }
        public virtual ICollection<Course> courses { get; set; }

        public string ratings { get; set; }

        public bool isEnrolled(int id)
        {
            foreach(Course course in courses)
            {
                if (course.id == id)
                    return true;
            }
            return false;
        }
    }
}