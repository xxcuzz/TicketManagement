using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TicketManagement.BusinessLogic.EntitiesDTO;
using TicketManagement.BusinessLogic.Services.Interfaces;
using Web.ViewModel;

namespace Web.Controllers
{
    [Authorize(Roles = "Event Manager")]
    public class ManageEventsController : Controller
    {
        private readonly ILayoutService _layoutService;
        private readonly IEventService _eventService;
        private readonly IMapper _mapper;

        public ManageEventsController(
            ILayoutService layoutService,
            IEventService eventService,
            IMapper mapper)
        {
            _layoutService = layoutService;
            _eventService = eventService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Layouts = new SelectList(_layoutService.GetAll(), "Id", "Description");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(EditEventViewModel model)
        {
            var eventDto = _mapper.Map<EditEventViewModel, EventDto>(model);

            await _eventService.CreateAsync(eventDto);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (_eventService.IsAnyEventSeatPurchased(id.Value))
            {
                return RedirectToAction("Index", "Home", new { errorMessage = "Event with purchased seats cannot be changed." });
            }

            ViewBag.Layouts = new SelectList(_layoutService.GetAll(), "Id", "Description");

            var eventDto = await _eventService.GetByIdAsync(id.Value);
            if (eventDto != null)
            {
                var model = _mapper.Map<EventDto, EditEventViewModel>(eventDto);
                return View(model);
            }

            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditEventViewModel model)
        {
            var eventDto = _mapper.Map<EditEventViewModel, EventDto>(model);

            await _eventService.UpdateAsync(eventDto);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (_eventService.IsAnyEventSeatPurchased(id.Value))
            {
                return RedirectToAction("Index", "Home", new { errorMessage = "Event with purchased seats cannot be deleted." });
            }

            var event1 = await _eventService.GetByIdAsync(id.Value);

            if (event1 != null)
            {
                await _eventService.DeleteAsync(event1);
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Error", "Home");
        }
    }
}
