using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SocialNetwork.Models
{
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public virtual User user { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Title must be between 3-30")]
        public string title { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 5, ErrorMessage = "Body must be between 5-255")]
        public string body { get; set; }

        public bool isPromoted { get; set; }

        public DateTime date { get; set; }


        public string Promoted()
        {
            if (isPromoted)
                return "success";
            else
                return "dark";
        }
    }
}