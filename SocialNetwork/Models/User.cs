using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialNetwork.Models
{   
    //This class describes the basic user
    //Inherating classes: Teacher and Student
    public class User
    {
        //TODO: Add regular expressions
        [Required]
		[StringLength(50,MinimumLength = 2,ErrorMessage ="Name should be between 2-50 characters")]
        public string name { get; set; }

        [Required]
        [Key]
        [Column(Order = 1)]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        public string password { get; set; }

        [Required]
        [Key]
        [Column(Order = 2)]
        [RegularExpression("^[0-9]{9}$", ErrorMessage = "ID should be 9 digits")]
        public string id { get; set; }

        [Required]
        public string role { get; set; }
    }
}