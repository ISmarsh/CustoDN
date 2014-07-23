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
            Nexus.Reload();
            return View(Nexus);
        }

        public ActionResult Add(Customer customer)
        {
            Nexus.Add(customer);
            Nexus.Commit();
            return RedirectToAction("Index","Customer");
        }
    }
}