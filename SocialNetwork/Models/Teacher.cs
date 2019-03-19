using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetwork.Models
{

    public class Teacher : User
    {
        public string courses { get; set; }

        public float avgRating { get; set; }


    }
}