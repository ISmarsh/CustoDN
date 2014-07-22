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
    public class SqlNexusRepository : INexusRepository
    {
        private const string DefaultConnection = 
                @"Server=tcp:vdyoh976qy.database.windows.net,1433;
                Database=CustoDN;
                User ID=custodian@vdyoh976qy;
                Password=DataNexus!;
                Trusted_Connection=False;
                Encrypt=True;
                Connection Timeout=30;";

        public SqlNexusRepository(string connectionString = null)
        { 
            Repo = new Repository(
            new DataContext(connectionString ?? DefaultConnection, new MappingConfig())); 
        }

        public Repository Repo { get; set; }

        public Customer Create(Customer customer)
        { return Repo.Context.Add(customer); }

        public Customer Read(Func<Customer, bool> predicate)
        { return Repo.Find(new FindSingleCustomer(predicate)); }

        public Customer Update(Customer customer)
        {
            return Repo.Context.Update(customer);
        }

        public Customer Delete(Customer customer)
        { return Repo.Context.Remove(customer); }

        public void Commit()
        { Repo.Context.Commit(); }

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

    public class FindCustomers : Query<Customer>
    {
        public FindCustomers(Func<Customer,bool> predicate)
        {
            ContextQuery = c => c.AsQueryable<Customer>().Where(predicate).AsQueryable();
        }
    }

    public class FindSingleCustomer : Scalar<Customer>
    {
        public FindSingleCustomer(Func<Customer, bool> predicate)
        {
            ContextQuery = c => c.AsQueryable<Customer>().SingleOrDefault(predicate);
        }
    }
}