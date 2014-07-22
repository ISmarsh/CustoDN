using System;
using System.Collections.Generic;
using System.Linq;

namespace CustoDN.Domain
{
    public interface INexusRepository
    {
        Customer Create(Customer customer);

        Customer ReadOne(Func<Customer, bool> predicate);

        List<Customer> ReadMany(Func<Customer, bool> predicate);

        List<Customer> ReadAll();

        Customer Update(Customer customer);

        Customer Delete(Customer customer);

        void Commit();
    }

    public class FakeNexusRepository : INexusRepository
    {
        public FakeNexusRepository()
        { Customers = new List<Customer>(); }

        public Customer Create(Customer customer)
        { Customers.Add(customer); return customer;}

        public Customer ReadOne(Func<Customer, bool> predicate)
        { return Customers.First(predicate); }

        public List<Customer> ReadMany(Func<Customer, bool> predicate)
        { return Customers.Where(predicate).ToList(); }

        public List<Customer> ReadAll()
        { return ReadMany(c => true); }

        public Customer Update(Customer customer)
        {
            var i = Customers.FindIndex(c => c.Id == customer.Id);
            Customers[i] = customer;
            return Customers[i];
        }

        public Customer Delete(Customer customer)
        { Customers.Remove(customer); return customer; }

        public void Commit() { }

        public List<Customer> Customers { get; set; }
    }
}