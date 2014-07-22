using System;

namespace CustoDN.Domain
{
    public class Customer
    {
        public Customer()
        { Id = Guid.NewGuid(); }

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string BillingAddress { get; set; }
    }
}
