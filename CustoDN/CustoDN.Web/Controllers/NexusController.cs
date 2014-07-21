using System.Web.Mvc;
using CustoDN.Domain;

namespace CustoDN.Web.Controllers
{
    public class NexusController : Controller
    {
        public NexusController(Nexus nexus)
        { Nexus = nexus; }

        public Nexus Nexus { get; set; }
    }
}
