using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Models
{   
    //This class describes the basic user
    //Inherating classes: Teacher and Student
    public class User
    {
        //TODO: Add regular expressions
        [Required]
		[StringLength(50,MinimumLength =5,ErrorMessage ="Name should be between 5-50 characters")]
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