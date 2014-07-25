using System;
using System.Linq;
using CustoDN.Domain;
using CustoDN.Web.Persistence;
using Highway.Data.Utilities;
using NUnit.Framework;

namespace CustoDN.Web.Tests.Persistence.When_Managing_Customers
{
    [TestFixture]
    public class Given_A_Nexus_With_An_Added_Customer
    {
        private Nexus nexus;
        private Customer customer;

        [SetUp]
        public void SetUp()
        {
            nexus = new Nexus(new CustoDNRepository());
            customer = A.Customer();
            nexus.Add(customer);
            nexus.Commit();
            nexus.Repository = new CustoDNRepository(); //New context
        }

        [TearDown]
        public void TearDown()
        {
            nexus.Delete(customer.Id);
            nexus.Commit();
        }

        private Customer GetCustomer()
        { return nexus.FindById(customer.Id); }

        [Test]
        public void And_Customer_Is_Added_It_Should_Be_Added()
        {
            Assert.That(GetCustomer().Equals(customer));
        }

        [Test]
        public void And_Customer_Is_Edited_It_Should_Be_Updated()
        {
            customer.CompanyName = "Kwik-E-Mart";
            nexus.Update(customer);
            nexus.Commit();
            var found = GetCustomer();
            Assert.That(found.Equals(customer));
        }

        [Test]
        public void And_Update_Is_Called_On_A_New_Customer_It_Is_Not_Added()
        {
            nexus.Delete(customer.Id); //For cleanup
            customer = new Customer();
            nexus.Update(customer);
            nexus.Commit();
            var again = GetCustomer();
            Assert.That(again, Is.Null);
        }

        [Test]
        public void And_UpdateOrAdd_Is_Called_On_A_New_Customer_It_Is_Added()
        {
            nexus.Delete(customer.Id); //For cleanup
            customer = new Customer();
            nexus.UpdateOrAdd(customer);
            nexus.Commit();
            var again = GetCustomer();
            Assert.That(again,Is.EqualTo(customer));
        }

        [Test]
        public void And_Customer_Is_Deleted_Then_It_Should_Be_Deleted()
        {
            nexus.Delete(customer.Id);
            nexus.Commit();
            Assert.That(GetCustomer(), Is.Null);
        }
    }
}
