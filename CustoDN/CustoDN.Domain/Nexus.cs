using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Highway.Data;
using Highway.Data.Contexts;

namespace CustoDN.Domain
{
    public class Nexus
    {
        public Nexus() : this(new Repository(new InMemoryDataContext())) { }

        public Nexus(Repository repo)
        {
            Repository = repo;
            Customers = new List<Customer>();
        }

        public Repository Repository { get; set; }

        public List<Customer> Customers { get; private set; }

        public Customer Add(Customer customer)
        {
            Customers.Add(Repository.Context.Add(customer));
            return customer;
        }

        public void Reload()
        {
            Customers = Repository.Find(new FindCustomers(c => true)).ToList();
        }

        public void UpdateOrAdd(Customer customer)
        {
            var match = new Func<Customer, bool>(c => c.Id == customer.Id);
            var here = Customers.SingleOrDefault(match);
            var there = Repository.Find(new FindSingleCustomer(match));
            (here ?? there ?? Add(customer)).Copy(customer);
        }

        public void Delete(Customer customer)
        {
            var match = new Func<Customer, bool>(c => c.Id == customer.Id);
            Repository.Context.Remove(
                Customers.SingleOrDefault(match) ?? Repository.Find(new FindSingleCustomer(match)));
            Customers.RemoveAll(c => c.Id == customer.Id);
        }

        public void Commit()
        {
            Repository.Context.Commit();
        }
    }

    public class FindCustomers : Query<Customer>
    {
        public FindCustomers(Func<Customer, bool> predicate)
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

    public class AnyCustomer : Scalar<bool>
    {
        public AnyCustomer(Func<Customer, bool> predicate)
        {
            ContextQuery = c => c.AsQueryable<Customer>().Any(predicate);
        }
    }
}