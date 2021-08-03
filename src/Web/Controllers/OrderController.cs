using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TicketManagement.BusinessLogic.Services;
using TicketManagement.BusinessLogic.Services.Interfaces;
using Web.Data;
using Web.Models;
using Web.ViewModel;

namespace Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IEventSeatService _eventSeatService;
        private readonly IUserTicketService _userTicketService;
        private readonly IdentityContext _identityContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPurchaseService _purchaseService;

        public OrderController(
            IEventSeatService eventSeatService,
            IUserTicketService userTicketService,
            IdentityContext identityContext,
            UserManager<ApplicationUser> userManager,
            IPurchaseService purchaseService)
        {
            _eventSeatService = eventSeatService;
            _userTicketService = userTicketService;
            _identityContext = identityContext;
            _userManager = userManager;
            _purchaseService = purchaseService;
        }

        public object OrderViewModel { get; private set; }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task Purchase(IEnumerable<int> eventSeatIds)
        {
            var price = await _purchaseService.GetFullPrice(eventSeatIds);
            var user = await _userManager.GetUserAsync(User);

            if (price > user.Balance)
            {
                return;
            }

            foreach (var s in eventSeatIds)
            {
                await _purchaseService.BuyTicket(user.Id, s);
            }

            user.Balance -= price;
            _identityContext.Users.Update(user);
            _identityContext.SaveChanges();
        }

        [HttpGet]
        public async Task<IActionResult> OrderHistoryView()
        {
            var user = await _userManager.GetUserAsync(User);

            var userTickets = _userTicketService.GetAllTicketsForUser(user.Id);

            var orderHistoryViewModelList = new List<OrderHistoryViewModel>();

            foreach (var userTicket in userTickets)
            {
                var seat = await _eventSeatService.GetById(userTicket.SeatId);
                orderHistoryViewModelList.Add(new OrderHistoryViewModel
                {
                    Description = await _eventSeatService.GetDescriptionOfCurruntEvent(userTicket.SeatId),
                    Row = seat.Row,
                    Number = seat.Number,
                    Price = await _eventSeatService.GetPriceForEventSeat(userTicket.SeatId),
                    OrderTime = userTicket.PurchaseTime,
                });
            }

            return View(orderHistoryViewModelList.AsEnumerable().Reverse());
        }
    }
}
