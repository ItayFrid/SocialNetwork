using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SocialNetwork.Models
{
    public class Progress
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public virtual Student student { get; set; }

        public virtual Course course { get; set; }
        [RegularExpression("^([0-9]|[1-9][0-9]|100)$", ErrorMessage = "Must be a number between 0-100")]
        public int prog { get; set; }

        public int grade { get; set; }
        public int getProgress()
        {
            return this.prog;
        }
    }
}