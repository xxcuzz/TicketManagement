using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web.Data;
using Web.Models;

namespace Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IdentityContext _identityContext;

        public UsersController(UserManager<ApplicationUser> userManager, IdentityContext identityContext)
        {
            _userManager = userManager;
            _identityContext = identityContext;
        }

        [HttpGet]
        public IActionResult TopUpBalance()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> TopUpBalance(string balance)
        {
            decimal d;
            try
            {
                balance = balance.Replace(",", CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator);
                d = decimal.Parse(balance, CultureInfo.InvariantCulture);
                if (d < 0)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }

            var user = await _userManager.GetUserAsync(User);
            user.Balance += d;
            if (user.Balance >= 1000.00M)
            {
                return RedirectToAction("Index", "Home");
            }

            _identityContext.Users.Update(user);
            await _identityContext.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
