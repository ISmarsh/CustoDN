using System.Web.Mvc;
using CustoDN.Domain;

namespace CustoDN.Web.Controllers
{
    public class CustomerController : NexusController
    {
        public CustomerController(Nexus nexus) : base(nexus) { }

        //
        // GET: /Customer/
        public ActionResult Index()
        {
            return View(Nexus);
        }

        public ActionResult Add(Customer customer)
        {
            Nexus.Add(customer);
            return View("Index",Nexus);
        }
    }
}