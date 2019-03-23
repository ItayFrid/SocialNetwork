using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SocialNetwork.Models
{
    public class Rating
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id { get; set; }
        [Required]
        public int courseId { get; set; }
        [Required]
		[EmailAddress]
        public string studentEmail { get; set; }
        [Required]
        [RegularExpression("^(10|[1-9])$", ErrorMessage ="Must be a number between 1-10")]
        public int rating { get; set; }
		[StringLength(255,MinimumLength =30,ErrorMessage ="Length must be between 30 to 255 characters")]
        public string comment { get; set; }
    }
}