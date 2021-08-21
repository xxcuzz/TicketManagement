using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TicketManagement.BusinessLogic.Services.Interfaces;
using Web.Data;
using Web.Models;

namespace Web.Controllers
{
    [Authorize(Roles = "Venue Manager")]
    public class ManageVenuesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IdentityContext _context;
        private readonly IUserTicketService _userTicketService;

        public ManageVenuesController(
            UserManager<ApplicationUser> userManager,
            IdentityContext context,
            IUserTicketService userTicketService)
        {
            _userManager = userManager;
            _context = context;
            _userTicketService = userTicketService;
        }

        public IActionResult Index(string errorMessage)
        {
            ViewBag.ErrorMessage = errorMessage;

            var us = _context.Users.ToList();
            return View(us);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var user = await _userManager.FindByIdAsync(id);
            var userRoles = await _userManager.GetRolesAsync(user);

            if (user.Balance > 0.0M)
            {
                return RedirectToAction("Index", "ManageVenues", new { errorMessage = "User with positive balance cannot be deleted." });
            }
            else if (userRoles.Count > 0)
            {
                return RedirectToAction("Index", "ManageVenues", new { errorMessage = "User with roles cannot be deleted." });
            }
            else if (await _userTicketService.IsAnyTicketStillAvailable(id))
            {
                return RedirectToAction("Index", "ManageVenues", new { errorMessage = "User with purchased tickets cannot be deleted." });
            }
            else
            {
                await _userTicketService.DeteleAllTicketsForUser(id);
                await _userManager.DeleteAsync(user);
                return RedirectToAction("Index", "ManageVenues");
            }
        }
    }
}
