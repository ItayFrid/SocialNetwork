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
    public class StudentControllerTests
    {
        [TestMethod()]
        public void IndexTest()
        {
            // Arrange
            StudentController controller = new StudentController();
            // Act
            ViewResult result = controller.Index() as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }       
    }
}