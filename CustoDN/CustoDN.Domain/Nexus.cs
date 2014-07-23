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
        }

        public Repository Repository { get; set; }

        public Customer Add(Customer customer)
        {
            return Repository.Context.Add(customer);
        }

        public Customer FindSingle(Func<Customer,bool> predicate)
        {
            return Repository.Find(new FindSingleCustomer(predicate));
        }

        public Customer FindById(Customer customer)
        { return FindSingle(c => c.Id == customer.Id); }

        public IEnumerable<Customer> FindMany(Func<Customer, bool> predicate)
        {
            return Repository.Find(new FindCustomers(predicate));
        }

        public IEnumerable<Customer> FindAll()
        { return FindMany(c => true); }

        public Customer Update(Customer customer)
        {
            var found = FindById(customer);
            if (found != null)
                found.Copy(customer);
            return found;
        }

        public Customer UpdateOrAdd(Customer customer)
        {
            return Update(customer) ?? Add(customer);
        }

        public void Delete(Customer customer)
        {
            var found = FindById(customer);
            if (found == null)
                return;
            Repository.Context.Remove(found);
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