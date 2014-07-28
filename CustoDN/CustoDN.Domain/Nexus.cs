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
        public Nexus(IRepository repo)
        {
            Repository = repo;
        }

        public IRepository Repository { get; set; }

        public Customer Add(Customer customer)
        {
            return Repository.Context.Add(customer);
        }

        public Customer FindSingle(Func<Customer,bool> predicate)
        {
            return Repository.Find(new FindSingleCustomer(predicate));
        }

        public Customer FindById(Guid id)
        { return FindSingle(c => c.Id == id); }

        public IEnumerable<Customer> FindMany(Func<Customer, bool> predicate)
        {
            return Repository.Find(new FindCustomers(predicate));
        }

        public IEnumerable<Customer> FindAll()
        { return FindMany(c => true); }

        public Customer Update(Customer customer)
        {
            var found = FindById(customer.Id);
            if (found != null)
                found.Copy(customer);
            return found;
        }

        public Customer UpdateOrAdd(Customer customer)
        {
            return Update(customer) ?? Add(customer);
        }

        public void Delete(Guid id)
        {
            var found = FindById(id);
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