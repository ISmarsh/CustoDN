using System.Collections.Generic;
using System.Linq;

namespace CustoDN.Domain
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

        public void Add(Customer customer)
        {
            if (Customers.Any(c => c.Id == customer.Id))
            { Update(customer); return; }
            Customers.Add(Repository.Create(customer)); 
        }

        public List<Customer> Customers { get; set; }

        public void Update(Customer customer)
        {
            var updated = Repository.Update(customer);
            var i = Customers.FindIndex(c => c.Id == updated.Id);
            Customers[i] = updated;
        }

        public void Delete(Customer customer)
        { Customers.Remove(Repository.Delete(customer)); }

        public void Commit()
        { Repository.Commit(); }

        public void Reload()
        { Customers = Repository.ReadAll(); }
    }
}