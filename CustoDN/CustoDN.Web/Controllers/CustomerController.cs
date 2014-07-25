using System.Web.Mvc;
using CustoDN.Domain;

namespace CustoDN.Web.Controllers
{
    [Authorize]
    public class CustomerController : NexusController
    {
        public CustomerController(Nexus nexus) : base(nexus) { }

        //
        // GET: /Customer/
        public ActionResult Index()
        {
            return View(Nexus.FindAll());
        }

        public ActionResult Submit(Customer customer)
        {

            Nexus.UpdateOrAdd(customer);
            Nexus.Commit();
            return RedirectToAction("Index","Customer");
        }
    }
}