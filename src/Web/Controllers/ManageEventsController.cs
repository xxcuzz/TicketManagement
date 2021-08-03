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
        private readonly IService<LayoutDto> _layoutService;
        private readonly IEventService _eventService;
        private readonly IMapper _mapper;

        public ManageEventsController(
            IService<LayoutDto> layoutService,
            IEventService eventService,
            IMapper mapper)
        {
            _layoutService = layoutService;
            _eventService = eventService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Layouts = new SelectList(_layoutService.GetAll(), "Id", "Description");
            if (id != null)
            {
                var eventDto = await _eventService.GetByIdAsync(id.Value);
                if (eventDto != null)
                {
                    var model = _mapper.Map<EventDto, EditEventViewModel>(eventDto);
                    return View(model);
                }
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditEventViewModel model)
        {
            var eventDto = _mapper.Map<EditEventViewModel, EventDto>(model);
            await _eventService.UpdateAsync(eventDto);
            return RedirectToAction("Index", "Home");
        }
    }
}
