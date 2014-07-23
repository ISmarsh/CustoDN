using System.Web.Mvc;
using CustoDN.Domain;
using CustoDN.Web.Controllers;
using NUnit.Framework;

namespace CustoDN.Web.Tests.Controllers
{
    [TestFixture]
    public class CustomerControllerTests
    {
        private CustomerController customerController;
        private Nexus nexus;

        [SetUp]
        public void SetUp()
        {
            nexus = new Nexus();
            customerController = new CustomerController(nexus);
        }

        [Test]
        public void Index()
        {
            var result = customerController.Index() as ViewResult;
            Assert.That(result,Is.Not.Null);
        }

        [Test]
        public void Add_ActionMethod_Should_Redirect_To_Customer_Index()
        {
            var customer = A.Customer();
            var result = customerController.Add(customer) as RedirectToRouteResult;
            Assert.That(result.RouteValues["controller"], Is.EqualTo("Customer"));
            Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));
        }
    }
}
