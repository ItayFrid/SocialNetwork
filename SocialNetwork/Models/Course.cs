using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SocialNetwork.Models
{
    public class Course
    {
        [Key]
        [Required]
        public int id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2-50")]
        public string name { get; set; }
        [Required]
        public float price { get; set; }
        [Required]
        public string tags { get; set; }
        public string ratings { get; set; }
        public float avgRating { get; set; }
        public string books { get; set; }

    }
}