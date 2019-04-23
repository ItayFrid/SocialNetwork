using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork;
using SocialNetwork.Controllers;
using System.Web.Mvc;
using SocialNetwork.Models;

namespace SocialNetwork.Controllers.Tests
{
    [TestClass()]
    public class LoginControllerTests
    {
        [TestMethod()]
        public void IndexTest()
        {
            // Arrange
            LoginController controller = new LoginController();
            // Act
            ViewResult result = controller.Index() as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }
        [TestMethod()]
        public void LoginTest()
        {
            //Arange
            LoginController login = new LoginController();
            //Act
            User user = new User
            {
                email = "fitay123@gmail.com",
                password = "123",
                id = "305360653"
            };
            ViewResult result = login.Login(user) as ViewResult;
            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void userExistsTest()
        {
            //Arrange
            LoginController login = new LoginController();
            //Act
            bool answer = login.userExists("fitay123@gmail.com");
            //Assert
            Assert.IsFalse(answer);
        }

        [TestMethod()]
        public void UserLoginTest()
        {
            // Arrange
            LoginController controller = new LoginController();
            // Act
            ViewResult result = controller.UserLogin() as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void UserRegisterTest()
        {
            // Arrange
            LoginController controller = new LoginController();
            // Act
            ViewResult result = controller.UserRegister() as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }
    }
}