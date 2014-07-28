using System.Linq;
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
            nexus = An.EmptyInMemoryNexus();
            customer = A.Customer();
            nexus.Add(customer);
            nexus.Commit();
        }

        [Test]
        public void Customer_Should_Be_Added()
        {
            Assert.That(nexus.FindAll().SingleOrDefault(c => c.Equals(customer)),Is.Not.Null);
        }

        [Test]
        public void And_Customer_Is_Edited_It_Should_Be_Updated()
        {
            customer.CompanyName = "Kwik-E-Mart";
            nexus.Update(customer);
            nexus.Commit();
            Assert.That(nexus.FindById(customer.Id).Equals(customer));
        }

        [Test]
        public void And_Customer_Is_Deleted_Then_It_Should_Be_Deleted()
        {
            nexus.Delete(customer.Id);
            nexus.Commit();
            Assert.That(nexus.FindById(customer.Id), Is.Null);
        }
    }
}
