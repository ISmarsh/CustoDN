﻿using System.Configuration;
using System.IO;
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
            nexus = An.EmptyDbNexus();
            customerController = new CustomerController(nexus);
        }

        [Test]
        public void Index()
        {
            var result = customerController.Index() as ViewResult;
            Assert.That(result,Is.Not.Null);
        }

        [Test]
        public void Submit_ActionMethod_Should_Redirect_To_Customer_Index()
        {
            var customer = A.Customer();
            var result = customerController.Submit(customer);
            //Assert.That(result.ViewName, Is.EqualTo("_Read"));
            //Assert.That(result.Model, Is.EqualTo(customer));
            Assert.That(result.RouteValues["controller"], Is.EqualTo("Customer"));
            Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));
        }

        [Test]
        public void Delete_ActionMethod_Should_Redirect_To_Customer_Index()
        {
            var customer = A.Customer();
            nexus.Add(customer);
            nexus.Commit();
            var result = customerController.Delete(customer.Id);
            Assert.That(result.RouteValues["controller"], Is.EqualTo("Customer"));
            Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));
        }
    }
}
