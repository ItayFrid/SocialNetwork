using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialNetwork.Controllers;
using SocialNetwork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SocialNetwork.Controllers.Tests
{
    [TestClass()]
    public class TeacherControllerTests
    {
        [TestMethod()]
        public void IndexTest()
        {
            // Arrange
            TeacherController controller = new TeacherController();
            // Act
            ViewResult result = controller.Index() as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void AddCourseTest()
        {
            // Arrange
            TeacherController controller = new TeacherController();
            // Act
            ViewResult result = controller.AddCourse() as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }
    }
}