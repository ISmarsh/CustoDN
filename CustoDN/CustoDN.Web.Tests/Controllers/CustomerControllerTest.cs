using System.Web.Mvc;
using CustoDN.Domain;
using CustoDN.Web.Controllers;
using NUnit.Framework;

namespace CustoDN.Web.Tests.Controllers
{
    [TestFixture]
    public class CustomerControllerTests
    {
        [Test]
        public void Index()
        {
            var customerController = new CustomerController(new Nexus());
            var result = customerController.Index() as ViewResult;
            Assert.That(result,Is.Not.Null);
        }
    }
}
