using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocialNetwork.Models;

namespace SocialNetwork.ViewModels
{
	public class ViewModel
	{
		public Course course { get; set; }
		public List<Course> courses { get; set; }

		public Enroll enroll { get; set; }
		public List<Enroll> enrolls { get; set; }

		public Rating rating { get; set; }
		public List<Rating> ratings { get; set; }

		public Student student { get; set; }
		public List<Student> students { get; set; }

		public Teacher teacher { get; set; }
		public List<Teacher> teachers { get; set; }

		public User user { get; set; }
		public List<User> users { get; set; }

        public Complaint complaint { get; set; }
        public List<Complaint> complaints { get; set; }

        public Message message { get; set; }
        public List<Message> messages { get; set; }

        public Progress progress { get; set; }
        public List<Progress> progresses { get; set; }
    }
}