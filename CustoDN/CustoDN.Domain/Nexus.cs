using System.Collections;
using System.Collections.Generic;

namespace CustoDN.Domain.Tests.CustomerTests
{
    public class Nexus
    {
        public Nexus() : this(new FakeNexusRepository()) { }
        public Nexus(INexusRepository repo)
        {
            Repository = repo;
            Customers = new List<Customer>();
        }

        public INexusRepository Repository { get; set; }

        public void AddCustomer(Customer customer)
        {
            Customers.Add(customer);
            Repository.AddCustomer(customer);
        }

        public List<Customer> Customers { get; set; }
    }

    public class FakeNexusRepository : INexusRepository
    {
        public FakeNexusRepository()
        { Customers = new List<Customer>(); }

        public void AddCustomer(Customer customer)
        {
            Customers.Add(customer);
        }

        public List<Customer> Customers { get; set; }
    }

    public interface INexusRepository
    {
        void AddCustomer(Customer customer);
    }
}