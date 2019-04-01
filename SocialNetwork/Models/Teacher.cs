using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetwork.Models
{

    public class Teacher : User
    {
        public List<Course> courses { get; set; }

        public float avgRating { get; set; }
        
        public bool authorized { get; set; }

        public string resume { get; set; }

    }
}