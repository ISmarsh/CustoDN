using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustoDN.Domain
{
    public class A
    {
        public static Customer Customer()
        {
            return new Customer()
            {
                FirstName = "Homer",
                LastName = "Simpson",
                BillingAddress = "742 Evergreen Terrace Springfield, Oregon",
                CompanyName = "Springfield Nuclear Power Plant",
                Email = "Chukylover53@AOL.com",
                Phone = "(503)555-8707"
            };
        }
    }
}
