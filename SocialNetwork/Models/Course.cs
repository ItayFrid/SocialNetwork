using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SocialNetwork.Models
{
    public class Course
    {
        public Course()
        {
            students = new List<Student>();
            ratings = new List<Rating>();
            studentProgress = new List<Progress>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public virtual Teacher teacher { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2-50")]
        public string name { get; set; }
        [Required]
        [Range(0, float.MaxValue, ErrorMessage = "Please enter a valid number")]
        public float price { get; set; }
        [Required]
        [StringLength(255, MinimumLength = 2, ErrorMessage = "Tags must be between 2-255")]
        public string tags { get; set; }
        public virtual ICollection<Rating> ratings { get; set; }
        public float avgRating { get; set; }
        public string books { get; set; }
        public virtual ICollection<Student> students { get; set; }
        public virtual ICollection<Progress> studentProgress { get; set; }
        public string AssistantEmail { get; set; }
        public bool AssistantAccept { get; set; }
        public string Enrolled(string id)
        {
            foreach(Student student in students)
            {
                if (student.id == id)
                    return "disabled";
            }
            return "";
        }

        public void calcAvg()
        {
            float tempAvg = 0;
            foreach (Rating rating in ratings)
                tempAvg += rating.rat;
            tempAvg /= ratings.Count;
            this.avgRating = tempAvg;
        }
        public Progress getProgress(string email)
        {
            foreach(Progress progress in studentProgress)
            {
                if (progress.student.email == email)
                    return progress;
            }
            return null;
        }

        public override string ToString()
        {
            string str ="Name:" + name + "<br>"
                       +"Price:" + price + "<br>"
                       + "Tags:" + tags + "<br>"
                       + "Rating:" + avgRating + "<br>";
            return str;
        }
    }
}