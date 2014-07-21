using NUnit.Framework;

namespace CustoDN.Domain.Tests.When_Adding_A_Customer
{
    [TestFixture]
    public class Given_A_New_Customer
    {
        private Nexus nexus;
        private Customer customer;

        [SetUp]
        public void Setup()
        {
            nexus = new Nexus();
            customer = new Customer();
            nexus.AddCustomer(customer);
        }

        [Test]
        public void Customer_Should_Be_Added()
        {
            Assert.That(nexus.Customers.Contains(customer));
        }
    }
}
