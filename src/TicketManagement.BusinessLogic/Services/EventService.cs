using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TicketManagement.BusinessLogic.EntitiesDTO;
using TicketManagement.BusinessLogic.Services.Interfaces;
using TicketManagement.BusinessLogic.Validation;
using TicketManagement.DataAccess.Entities;
using TicketManagement.DataAccess.InterfacesRepository;

namespace TicketManagement.BusinessLogic.Services
{
    public class EventService : IEventService
    {
        private readonly IRepository<Event> _eventRepo;
        private readonly IMapper _mapper;
        private readonly IEventAreaService _eventAreaService;
        private readonly IEventSeatService _eventSeatService;
        private readonly IAreaService _areaService;
        private readonly ISeatService _seatService;
        private readonly IRepository<Layout> _layoutRepo;

        public EventService(
            IRepository<Event> eventRepo,
            IEventAreaService eventAreaService,
            IEventSeatService eventSeatService,
            IAreaService areaService,
            ISeatService seatService,
            IRepository<Layout> layoutRepo,
            IMapper mapper)
        {
            _eventRepo = eventRepo;
            _eventAreaService = eventAreaService;
            _eventSeatService = eventSeatService;
            _areaService = areaService;
            _seatService = seatService;
            _layoutRepo = layoutRepo;
            _mapper = mapper;
        }

        public async Task<bool> CreateAsync(EventDto item)
        {
            if (item == null)
            {
                return false;
            }

            var event1 = _mapper.Map<EventDto, Event>(item);

            var layout = await _layoutRepo.GetByIdAsync(item.LayoutId);

            var events = GetEventsInLayout(item.LayoutId);

            var areas = _areaService.GetAreasByLayoutId(item.LayoutId);
            var seats = new List<SeatDto>();
            foreach (var area in areas)
            {
                seats.AddRange(_seatService.GetSeatsByAreaId(area.Id));
            }

            return EventValidator.Validate(event1, layout, events, seats) && await _eventRepo.AddAsync(event1);
        }

        public async Task<bool> UpdateAsync(EventDto item)
        {
            var event1 = _mapper.Map<EventDto, Event>(item);
            var layout = await _layoutRepo.GetByIdAsync(item.LayoutId);

            var events = GetEventsInLayout(item.LayoutId);

            var areas = _areaService.GetAreasByLayoutId(item.LayoutId);
            var seats = new List<SeatDto>();
            foreach (var area in areas)
            {
                seats.AddRange(_seatService.GetSeatsByAreaId(area.Id));
            }

            return EventValidator.Validate(event1, layout, events, seats) && await _eventRepo.UpdateAsync(event1);
        }

        public async Task<bool> DeleteAsync(EventDto item)
        {
            var event1 = _mapper.Map<EventDto, Event>(item);

            return await _eventRepo.DeleteAsync(event1);
        }

        public IEnumerable<EventDto> GetAll()
        {
            var events = _eventRepo.GetAll();
            if (events == null)
            {
                return Enumerable.Empty<EventDto>();
            }

            var eventDtos = _mapper.Map<List<Event>, List<EventDto>>(events.ToList());

            return eventDtos;
        }

        public async Task<EventDto> GetByIdAsync(int id)
        {
            var eventItem = await _eventRepo.GetByIdAsync(id);
            var eventDto = _mapper.Map<Event, EventDto>(eventItem);
            return eventDto;
        }

        public bool IsAnySeatAvailable(EventDto item)
        {
            var eventAreaDtos = _eventAreaService.GetAllEventAreasForEvent(item.Id);

            if (eventAreaDtos == null)
            {
                return false;
            }

            foreach (var eventArea in eventAreaDtos)
            {
                var eventSeatDtos = _eventSeatService.GetAllEventSeatsForEventArea(eventArea.Id);

                if (eventSeatDtos.Any(es => es.State == 0))
                {
                    return true;
                }
            }

            return false;
        }

        public IQueryable<Event> GetEventsInLayout(int layoutId)
        {
            return _eventRepo.GetAll().Where(e => e.LayoutId == layoutId);
        }
    }
}
