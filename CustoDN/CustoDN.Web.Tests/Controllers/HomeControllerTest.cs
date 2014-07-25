using System.Web.Mvc;
using CustoDN.Web.Controllers;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace CustoDN.Web.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerTest
    {
        [Test]
        public void Home_Index_Redirects_To_Customer_Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            var result = controller.Index() as RedirectToRouteResult;

            // Assert
            Assert.AreEqual(result.RouteValues["controller"], "Customer");
            Assert.AreEqual(result.RouteValues["action"], "Index");
        }

        [Test]
        public void About()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [Test]
        public void Contact()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
