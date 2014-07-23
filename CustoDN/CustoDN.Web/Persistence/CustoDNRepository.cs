using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using CustoDN.Domain;
using Highway.Data;

namespace CustoDN.Web.Persistence
{
    public class CustoDNRepository : Repository
    {
        public CustoDNRepository() : base(new DataContext(DefaultConnectionString, new MappingConfig())) { }

        private const string DefaultConnectionString = 
                @"Server=tcp:vdyoh976qy.database.windows.net,1433;
                Database=CustoDN;
                User ID=custodian@vdyoh976qy;
                Password=DataNexus!;
                Trusted_Connection=False;
                Encrypt=True;
                Connection Timeout=30;";

        private void TimsExamples()
        {
            //CREATE WITH ENTITY FRAMEWORK
            var context = new CustomerDBContext();
            context.Customers.Add(A.Customer());
            context.SaveChanges();

            //WITH HIGHWAY
            var mappingConfig = new MappingConfig();
            var hontext = new DataContext("", mappingConfig);
            var Repo = new Repository(hontext);
            Repo.Context.Add(A.Customer());
            Repo.Context.Commit();

            //READ WITH ENTITY FRAMEWORK
            var cs = context.Customers.Include(c => c.Email); //Email is just a string, but this is how you fill in child objects

            //WITH HIGHWAY
            var homerWay = Repo.Find(new FindSingleCustomer(c => c.FirstName == "Homer"));

            //UPDATE WITH ENTITY FRAMEWORK
            var homer = context.Customers.First();
            homer.CompanyName = "Kwik-E-Mart";
            context.SaveChanges();

            //WITH HIGHWAY
            homerWay.CompanyName = "Kwik-E-Mart";
            Repo.Context.Commit();

            //DELETE WITH ENTITY FRAMEWORK
            //Make sure to remove/clear child objects/lists first, no need to save between that and removing parent
            context.Customers.Remove(homer);
            context.SaveChanges();

            //WITH HIGHWAY
            Repo.Context.Remove(homerWay);
            Repo.Context.Commit();
        }
    }

    //FOR ENTITY FRAMEWORK
    public class CustomerDBContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
    }

    //FOR HIGHWAY
    public class MappingConfig : IMappingConfiguration
    {
        public void ConfigureModelBuilder(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(); //.HasMany(c => c.Many).WithRequired(m => m.Customer);
        }
    }
}