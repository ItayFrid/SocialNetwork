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
            modelBuilder.Entity<Course>().ToTable("Courses");
            modelBuilder.Entity<Enroll>().ToTable("Enrolls");
            modelBuilder.Entity<Rating>().ToTable("Ratings");
            modelBuilder.Entity<Student>().ToTable("Students");
            modelBuilder.Entity<Teacher>().ToTable("Teachers");
            modelBuilder.Entity<User>().ToTable("Users");

        }

        /*Database sets*/
        public DbSet<Course> courses { get; set; }
        public DbSet<Enroll> enrolls { get; set; }
        public DbSet<Rating> ratings { get; set; }
        public DbSet<Student> students { get; set; }
        public DbSet<Teacher> teachers { get; set; }
        public DbSet<User> users { get; set; }

    }
}