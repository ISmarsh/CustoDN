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
            nexus = new Nexus(new CustoDNRepository());
            customer = A.Customer();
            nexus.Add(customer);
            nexus.Commit();
            nexus.Repository = new CustoDNRepository(); //New context
            nexus.Customers.Clear(); //To simulate all new request
        }

        [TearDown]
        public void TearDown()
        {
            var found = GetCustomer();
            if (found != null)
            {
                nexus.Delete(found);
                nexus.Commit();
            }
        }

        private Customer GetCustomer()
        { return nexus.Repository.Find(new FindSingleCustomer(c => c.Id == customer.Id)); }

        [Test]
        public void And_Customer_Is_Added_It_Should_Be_Added()
        {
            Assert.That(GetCustomer().Equals(customer));
        }

        [Test]
        public void Reload_Should_Load_Added_Customers()
        {
            nexus.Reload();
            Assert.That(nexus.Customers.SingleOrDefault(c => c.Equals(customer)),Is.Not.Null);
        }

        [Test]
        public void And_Customer_Is_Edited_Without_Reloading_It_Should_Be_Updated()
        {
            customer.CompanyName = "Kwik-E-Mart";
            nexus.UpdateOrAdd(customer);
            nexus.Commit();
            var found = GetCustomer();
            Assert.That(found.Equals(customer));
        }

        [Test]
        public void And_Customer_Is_Edited_After_Reloading_It_Should_Be_Updated()
        {
            nexus.Reload();
            customer.CompanyName = "Kwik-E-Mart";
            nexus.UpdateOrAdd(customer);
            nexus.Commit();
            var again = GetCustomer();
            Assert.That(again.Equals(customer));
        }

        [Test]
        public void And_Update_Is_Called_On_A_New_Customer_It_Is_Added()
        {
            customer = new Customer();
            nexus.UpdateOrAdd(customer);
            nexus.Commit();
            var again = GetCustomer();
            Assert.That(again,Is.EqualTo(customer));
        }

        [Test]
        public void And_Customer_Is_Deleted_Without_Reloading_Then_It_Should_Be_Deleted()
        {
            nexus.Delete(customer);
            nexus.Commit();
            Assert.That(GetCustomer(), Is.Null);
        }

        [Test]
        public void And_Customer_Is_Deleted_After_Reloading_Then_It_Should_Be_Deleted()
        {
            nexus.Reload();
            nexus.Delete(customer);
            nexus.Commit();
            Assert.That(GetCustomer(), Is.Null);
        }
    }
}
