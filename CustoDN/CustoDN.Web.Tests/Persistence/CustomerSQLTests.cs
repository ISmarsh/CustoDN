using System;
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
        }

        [TearDown]
        public void TearDown()
        {
            if (nexus.Repository.Read(c => c.Id == customer.Id) != null)
            {
                nexus.Delete(customer);
                nexus.Commit();
            }
        }

        [Test]
        public void And_Customer_Is_Added_It_Should_Be_Added()
        {
            var found = nexus.Repository.Read(c => c.Id == customer.Id);
            Assert.That(found,Is.EqualTo(customer));
        }

        [Test]
        public void And_Customer_Is_Edited_It_Should_Be_Updated()
        {
            customer.CompanyName = "Kwik-E-Mart";
            nexus.Repository = new SqlNexusRepository(); //New context
            nexus.Update(customer);
            nexus.Commit();
            Assert.That(nexus.Repository.Read(c => c.Id == customer.Id), Is.EqualTo(customer));
        }

        [Test]
        public void And_Customer_Is_Deleted_Then_It_Should_Be_Deleted()
        {
            nexus.Delete(customer);
            nexus.Commit();
            Assert.That(nexus.Repository.Read(c => c.Id == customer.Id), Is.Null);
        }
    }
}
