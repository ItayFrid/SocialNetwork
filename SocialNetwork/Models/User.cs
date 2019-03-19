using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Models
{   
    //This class describes the basic user
    //Inherating classes: Admin, Teacher and Student
    public class User
    {
        //TODO: Add regular expressions
        [Required]
        public string name { get; set; }

        [Required]
        [Key]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        public string password { get; set; }

        [Required]
        public string role { get; set; }
    }
}