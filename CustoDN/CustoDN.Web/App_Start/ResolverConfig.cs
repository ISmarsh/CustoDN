using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using CustoDN.Domain;
using Highway.Data;

namespace CustoDN.Web.App_Start
{
    public class ResolverConfig
    {
        private static Nexus nexus;

        public static void Register()
        {
            //CreateDummyNexus();

            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof (ResolverConfig).Assembly);
            builder.Register<IMappingConfiguration>(c => new MappingConfig());
            builder.Register<IDataContext>(c => new DataContext(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString,
                                                c.Resolve<IMappingConfiguration>()));
            builder.Register<IRepository>(c => new Repository(c.Resolve<IDataContext>()));
            //builder.Register(c => nexus);
            builder.Register(c => new Nexus(c.Resolve<IRepository>()));

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        private static void CreateDummyNexus()
        {
            nexus = new Nexus(new Repository(new DataContext(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString,new MappingConfig())));
            nexus.Add(A.Customer());
            nexus.Commit();
        }
    }
}