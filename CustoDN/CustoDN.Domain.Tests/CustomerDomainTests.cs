using Highway.Data.Utilities;
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
            customer = A.Customer();
            nexus.Add(customer);
        }

        [Test]
        public void Customer_Should_Be_Added()
        {
            Assert.That(nexus.Customers.Contains(customer));
        }

        [Test]
        public void And_Customer_Is_Edited_It_Should_Be_Updated()
        {
            var edited = customer.Clone();
            edited.CompanyName = "Kwik-E-Mart";
            nexus.UpdateOrAdd(edited);
            Assert.That(nexus.Customers.Find(c => c.Id == customer.Id),Is.EqualTo(edited));
        }

        [Test]
        public void And_Customer_Is_Deleted_Then_It_Should_Be_Deleted()
        {
            nexus.Delete(customer);
            Assert.That(nexus.Customers.Contains(customer),Is.False);
        }
    }
}
