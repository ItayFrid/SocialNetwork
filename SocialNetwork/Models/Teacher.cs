using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SocialNetwork.Models
{

    public class Teacher : User
    {
        public Teacher()
        {
            courses = new List<Course>();
        }

        public virtual ICollection<Course> courses { get; set; }

        public int Rating { get; set; }

        public int numRating { get; set; }

        public bool authorized { get; set; }

        public string resume { get; set; }

        public bool mobile { get; set; }
        public string isAuthorized()
        {
            if (authorized)
                return "";
            else
                return "disabled";
        }

        public float AvgRating()
        {
            if (numRating != 0)
                return Rating / numRating;
            else
                return 0;
        }

        public string isMobile()
        {
            if (mobile)
                return "Mobile";
            else
                return "Not Mobile";
        }
    }
}