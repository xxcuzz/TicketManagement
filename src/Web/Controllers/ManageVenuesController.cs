using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class ManageVenuesController : Controller
    {
        [Authorize(Roles = "Venue Manager")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
