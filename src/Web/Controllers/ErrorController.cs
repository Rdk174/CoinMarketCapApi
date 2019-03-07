using System.Web.Mvc;
using BussinesFacade;
using Web.Models;

namespace Web.Controllers
{
    public class ErrorController : Controller
    {
        // GET
        public ActionResult Error(Status status)
        {
            return
            View(status);
        }
    }
}