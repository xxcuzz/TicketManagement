using System.Threading.Tasks;
using EventManagementApi.EntitiesDTO;
using EventManagementApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventManagementApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly IEventSeatService _eventSeatService;
        private readonly IEventAreaService _eventAreaService;

        public EventsController(
            IEventService eventService,
            IEventSeatService eventSeatService,
            IEventAreaService eventAreaService)
        {
            _eventService = eventService;
            _eventSeatService = eventSeatService;
            _eventAreaService = eventAreaService;
        }

        /// <summary>
        /// Gets all events.
        /// </summary>
        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetEvents()
        {
            var result = _eventService.GetAll();
            return Ok(result);
        }

        /// <summary>
        /// Gets event by id.
        /// </summary>
        /// <returns>Event.</returns>
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvent(int id)
        {
            var result = await _eventService.GetByIdAsync(id);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        /// <summary>
        /// Gets all seats for Event.
        /// </summary>
        /// <param name="id">Id of the event.</param>
        /// <returns>Seats.</returns>
        [Authorize]
        [HttpGet("{id}/seats")]
        public IActionResult GetSeats(int id)
        {
            var result = _eventSeatService.GetAllEventSeatsForEvent(id);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        /// <summary>
        /// Gets all areas for Event.
        /// </summary>
        /// <param name="id">Id of the event.</param>
        /// <returns>Areas.</returns>
        [Authorize]
        [HttpGet("{id}/areas")]
        public IActionResult GetAreas(int id)
        {
            var result = _eventAreaService.GetAllEventAreasForEvent(id);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        /// <summary>
        /// Gets seat by id.
        /// </summary>
        [Authorize]
        [HttpGet("seats/{id}")]
        public async Task<IActionResult> GetSeat(int id)
        {
            var result = await _eventSeatService.GetById(id);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        /// <summary>
        /// Adds new event.
        /// </summary>
        /// <param name="model">Event.</param>
        /// <returns>Created Event.</returns>
        // [Authorize(Roles = "Event Manager")]
        [HttpPost]
        public async Task<IActionResult> InsertEvent([FromForm] EventDto model)
        {
            var result = await _eventService.CreateAsync(model);
            if (result)
            {
                return CreatedAtAction(nameof(GetEvent), new { id = model.Id }, model);
            }

            return BadRequest();
        }

        /// <summary>
        /// Updates event.
        /// </summary>
        /// <param name="model">Event.</param>
        /// [Authorize(Roles = "Event Manager")]
        [HttpPut]
        public async Task<IActionResult> Update([FromForm] EventDto model)
        {
            var result = await _eventService.UpdateAsync(model);
            if (result)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        /// <summary>
        /// Updates seat.
        /// </summary>
        /// <param name="model">Seat.</param>
        /// [Authorize(Roles = "Event Manager")]
        [HttpPut]
        public async Task<IActionResult> UpdateSeat([FromForm] EventSeatDto model)
        {
            var result = await _eventSeatService.ChangeEventSeatState(model);
            if (result)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        /// <summary>
        /// Deletes event.
        /// </summary>
        /// [Authorize(Roles = "Event Manager")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var result = await _eventService.DeleteAsync(id);
            if (result)
            {
                return Ok(result);
            }

            return BadRequest();
        }
    }
}
