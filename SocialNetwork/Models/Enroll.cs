using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetwork.Models
{   

    //This class holds refrences of teacher, course and students
    //Instance of this class is created when the first student signs up to the course

    public class Enroll
    {

        public string teacherId { get; set; }

        public string courseId { get; set; }

        public string studentsId { get; set; }

    }
}