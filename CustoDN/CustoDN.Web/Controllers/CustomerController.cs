using System;
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

        public ActionResult Edit(Guid id)
        {
            return View("Edit", Nexus.FindById(id));
        }

        //public PartialViewResult Edit(Guid id)
        //{
        //    return PartialView("_Create", Nexus.FindById(id));
        //}

        public RedirectToRouteResult Submit(Customer customer)
        {

            Nexus.UpdateOrAdd(customer);
            Nexus.Commit();
            return RedirectToAction("Index", "Customer");
        }

        //public PartialViewResult Submit(Customer customer)
        //{

        //    Nexus.UpdateOrAdd(customer);
        //    Nexus.Commit();
        //    return PartialView("_Read",customer);
        //}

        public RedirectToRouteResult Delete(Guid id)
        {
            Nexus.Delete(id);
            Nexus.Commit();
            return RedirectToAction("Index", "Customer");
        }
    }
}