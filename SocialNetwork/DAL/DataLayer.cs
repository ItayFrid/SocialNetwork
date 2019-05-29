using SocialNetwork.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SocialNetwork.DAL
{
    public class DataLayer    :   DbContext
    {

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Course>().ToTable("Courses").HasMany(x => x.students);
			modelBuilder.Entity<Rating>().ToTable("Ratings");
			modelBuilder.Entity<Student>().ToTable("Students").HasMany(x =>x.courses);
            modelBuilder.Entity<Teacher>().ToTable("Teachers").HasMany(x => x.courses);
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Complaint>().ToTable("Complaints");
            modelBuilder.Entity<Message>().ToTable("Messages");
            modelBuilder.Entity<Progress>().ToTable("Progresses");
            modelBuilder.Entity<Post>().ToTable("Posts");
            base.OnModelCreating(modelBuilder);
        }
        
		/*Database sets*/
		public DbSet<Course> courses { get; set; }
		//public DbSet<Enroll> enrolls { get; set; }
		public DbSet<Rating> ratings { get; set; }
		public DbSet<Student> students { get; set; }
        public DbSet<Teacher> teachers { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Complaint> complaints { get; set; }
        public DbSet<Message> messages { get; set; }
        public DbSet<Progress> progresses { get; set; }
        public DbSet<Post> posts { get; set; }
    }
}