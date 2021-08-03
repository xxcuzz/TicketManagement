using System.Collections.Generic;
using System.Threading.Tasks;
using EventManagementApi.EntitiesDTO;

namespace EventManagementApi.Services
{
    public class EventManagementService
    {
        private readonly EventService _eventService;
        private readonly EventAreaService _eventAreaService;
        private readonly EventSeatService _eventSeatService;
        private readonly SeatService _seatService;
        private readonly AreaService _areaService;

        public EventManagementService(
            EventService eventService,
            EventAreaService eventAreaService,
            EventSeatService eventSeatService,
            SeatService seatService,
            AreaService areaService)
        {
            _eventService = eventService;
            _eventAreaService = eventAreaService;
            _eventSeatService = eventSeatService;
            _seatService = seatService;
            _areaService = areaService;
        }

        public async Task<EventDto> GetEventById(int id)
        {
            return await _eventService.GetByIdAsync(id);
        }

        public IEnumerable<EventSeatDto> GetSeats(int id)
        {
            return _eventSeatService.GetAllEventSeatsForEvent(id);
        }

        public IEnumerable<EventAreaDto> GetAreas(int id)
        {
            return _eventAreaService.GetAllEventAreasForEvent(id);
        }
    }
}
