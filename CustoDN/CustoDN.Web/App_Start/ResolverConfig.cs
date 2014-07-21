using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using CustoDN.Domain;

namespace CustoDN.Web.App_Start
{
    public class ResolverConfig
    {
        private static Nexus nexus;

        public static void Register()
        {
            CreateDummyNexus();

            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof (ResolverConfig).Assembly);
            builder.Register(c => nexus);
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        private static void CreateDummyNexus()
        {
            nexus = new Nexus();
            nexus.AddCustomer(new Customer()
            {
                FirstName = "Homer",
                LastName = "Simpson",
                BillingAddress = "742 Evergreen Terrace Springfield, Oregon",
                CompanyName = "Springfield Nuclear Power Plant",
                Email = "Chukylover53@AOL.com",
                Phone = "(503)555-8707"
            });
        }
    }
}