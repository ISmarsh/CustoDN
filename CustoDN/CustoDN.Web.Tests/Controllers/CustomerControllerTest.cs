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
        public void Add_ActionMethod_Should_Add_Customer()
        {
            var customer = A.Customer();
            var result = customerController.Add(customer) as ViewResult;
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Model, Is.EqualTo(nexus));
            Assert.That((result.Model as Nexus).Customers.Contains(customer));
        }
    }
}
