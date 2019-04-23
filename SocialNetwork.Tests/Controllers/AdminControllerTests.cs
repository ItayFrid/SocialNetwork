using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialNetwork.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SocialNetwork.Controllers.Tests
{
    [TestClass()]
    public class AdminControllerTests
    {
        [TestMethod()]
        public void ShowUsersTest()
        {
            // Arrange
            AdminController controller = new AdminController();
            // Act
            ViewResult result = controller.ShowUsers() as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void AdminComplaintsTest()
        {
            // Arrange
            AdminController controller = new AdminController();
            // Act
            ViewResult result = controller.AdminComplaints() as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }
    }
}