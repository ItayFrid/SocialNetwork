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
    public class RatingControllerTests
    {
        [TestMethod()]
        public void IndexTest()
        {
            // Arrange
            RatingController controller = new RatingController();
            // Act
            ViewResult result = controller.Index() as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void RatingRegisterTest()
        {
            // Arrange
            RatingController controller = new RatingController();
            // Act
            ViewResult result = controller.RatingRegister() as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }
    }
}