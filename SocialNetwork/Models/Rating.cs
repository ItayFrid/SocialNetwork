using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SocialNetwork.Models
{
    public class Rating
    {
        [Key]
        [Required]
        public int id { get; set; }
        [Required]
        public int courseId { get; set; }
        [Required]
        public int studentId { get; set; }
        [Required]
        public int rating { get; set; }
        [Required]
        public string comment { get; set; }
    }
}