using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetwork.Models
{
    public class Student : User
    {

        public string courses { get; set; }

        public string ratings { get; set; }

    }
}