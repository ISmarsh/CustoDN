using System;
using System.Linq;
using CustoDN.Domain;
using CustoDN.Web.Persistence;
using Highway.Data.Utilities;
using NUnit.Framework;

namespace CustoDN.Web.Tests.Persistence.When_Managing_Customers
{
    [TestFixture]
    public class Given_A_Nexus_With_Customers
    {
        private Nexus nexus;
        private Customer customer;

        [SetUp]
        public void SetUp()
        {
            nexus = new Nexus(new SqlNexusRepository());
            customer = A.Customer();
            nexus.Add(customer);
            nexus.Commit();
            nexus.Repository = new SqlNexusRepository(); //New context
        }

        [TearDown]
        public void TearDown()
        {
            var found = nexus.Customers.FirstOrDefault(c => c.Id == customer.Id);
            if (found != null)
            {
                nexus.Delete(found);
                nexus.Commit();
            }
        }

        private Customer getCustomer()
        { return new SqlNexusRepository().ReadOne(c => c.Id == customer.Id); }

        [Test]
        public void And_Customer_Is_Added_It_Should_Be_Added()
        {
            Assert.That(getCustomer().Equals(customer));
        }

        [Test]
        public void And_Customer_Is_Added_It_Should_Be_Loaded_In_By_Reload()
        {
            nexus.Reload();
            Assert.That(nexus.Customers.Contains(customer));
        }

        [Test]
        public void And_Customer_Is_Edited_It_Should_Be_Updated()
        {
            customer.CompanyName = "Kwik-E-Mart";
            nexus.Update(customer);
            nexus.Commit();
            Assert.That(getCustomer().Equals(customer));
        }

        [Test]
        public void And_Customer_Is_Deleted_Then_It_Should_Be_Deleted()
        {
            nexus.Delete(customer);
            nexus.Commit();
            Assert.That(getCustomer(), Is.Null);
        }
    }
}
