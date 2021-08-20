using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketManagement.BusinessLogic.Services.Interfaces;
using Web.Models;
using Web.ViewModel;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEventService _eventService;
        private readonly IEventAreaService _eventAreaService;
        private readonly IEventSeatService _eventSeatService;

        public HomeController(
            IEventService eventService,
            IEventAreaService eventAreaService,
            IEventSeatService eventSeatService)
        {
            _eventService = eventService;
            _eventAreaService = eventAreaService;
            _eventSeatService = eventSeatService;
        }

        [HttpGet]
        public IActionResult Index(string errorMessage)
        {
            ViewBag.ErrorMessage = errorMessage;

            var eventDtos = _eventService.GetAll().Where(eventdto => eventdto.EventStart > DateTime.Now);

            var ivmList = eventDtos.Select(i => new IndexViewModel
            {
                Id = i.Id,
                Name = i.Name,
                Description = i.Description,
                TicketAvailable = _eventService.IsAnySeatAvailable(i) ? "Tickets are available" : "No tickets",
            }).ToList();

            return View(ivmList);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> EventView(int id)
        {
            var eventDto = await _eventService.GetByIdAsync(id);

            var eventViewModel = new EventViewModel
            {
                Id = eventDto.Id,
                Name = eventDto.Name,
                Description = eventDto.Description,
                StartTime = eventDto.EventStart,
                EndTime = eventDto.EventEnd,
                EventAreas = _eventAreaService.GetAllEventAreasForEvent(id),
                EventSeats = _eventSeatService.GetAllEventSeatsForEvent(id),
            };
            return View(eventViewModel);
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
