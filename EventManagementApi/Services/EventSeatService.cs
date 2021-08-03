using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventManagementApi.EntitiesDTO;
using EventManagementApi.Services.Interfaces;
using TicketManagement.DataAccess.Entities;
using TicketManagement.DataAccess.InterfacesRepository;

namespace EventManagementApi.Services
{
    public class EventSeatService : IEventSeatService
    {
        private readonly IRepository<EventSeat> _eventSeatRepo;
        private readonly IRepository<EventArea> _eventAreaRepo;
        private readonly IEventAreaService _eventAreaService;
        private readonly IRepository<Event> _eventRepo;
        private readonly IMapper _mapper;

        public EventSeatService(
            IRepository<EventSeat> eventSeatRepo,
            IRepository<EventArea> eventAreaRepo,
            IEventAreaService eventAreaService,
            IRepository<Event> eventRepo,
            IMapper mapper)
        {
            _eventSeatRepo = eventSeatRepo;
            _eventAreaRepo = eventAreaRepo;
            _eventAreaService = eventAreaService;
            _eventRepo = eventRepo;
            _mapper = mapper;
        }

        public async Task<bool> ChangeEventSeatState(EventSeatDto item)
        {
            var eventSeat = _mapper.Map<EventSeatDto, EventSeat>(item);
            eventSeat.State = eventSeat.State == 0 ? 1 : 0;
            return await _eventSeatRepo.UpdateAsync(eventSeat);
        }

        public IEnumerable<EventSeatDto> GetAll()
        {
            var eventSeats = _eventSeatRepo.GetAll().ToList();
            var eventSeatDtos = _mapper.Map<List<EventSeat>, List<EventSeatDto>>(eventSeats);
            return eventSeatDtos;
        }

        public IEnumerable<EventSeatDto> GetAllEventSeatsForEventArea(int eventAreaId)
        {
            var eventSeats = GetAllEventSeatsByEventArea(eventAreaId).ToList();
            var eventSeatDtos = _mapper.Map<List<EventSeat>, List<EventSeatDto>>(eventSeats);

            return eventSeatDtos;
        }

        public IEnumerable<EventSeatDto> GetAllEventSeatsForEvent(int eventId)
        {
            var eventSeatDtos = new List<EventSeatDto>();
            var eventAreasForEvent = _eventAreaService.GetAllEventAreasForEvent(eventId);

            foreach (var eventArea in eventAreasForEvent)
            {
                var seatsInEventArea = GetAllEventSeatsByEventArea(eventArea.Id);
                foreach (var seat in seatsInEventArea)
                {
                    eventSeatDtos.Add(_mapper.Map<EventSeat, EventSeatDto>(seat));
                }
            }

            return eventSeatDtos;
        }

        public async Task<EventSeatDto> GetById(int id)
        {
            var eventSeatItem = await _eventSeatRepo.GetByIdAsync(id);
            var eventSeatDto = _mapper.Map<EventSeat, EventSeatDto>(eventSeatItem);
            return eventSeatDto;
        }

        public async Task<decimal> GetPriceForEventSeat(int id)
        {
            var eventSeat = await _eventSeatRepo.GetByIdAsync(id);
            var eventAreaId = eventSeat.EventAreaId;
            var result = await _eventAreaService.GetEventAreaPrice(eventAreaId);
            return result;
        }

        public async Task<string> GetDescriptionOfCurruntEvent(int id)
        {
            var eventSeat = await _eventSeatRepo.GetByIdAsync(id);
            var eventArea = await _eventAreaRepo.GetByIdAsync(eventSeat.EventAreaId);
            var eventItem = await _eventRepo.GetByIdAsync(eventArea.EventId);
            return eventItem.Description;
        }

        public IQueryable<EventSeat> GetAllEventSeatsByEventArea(int eventAreaId)
        {
            return _eventSeatRepo.GetAll().Where(es => es.EventAreaId == eventAreaId);
        }

        public async Task<int> GetCurrentEventId(int eventSeatId)
        {
            var eventSeat = await _eventSeatRepo.GetByIdAsync(eventSeatId);
            var eventArea = await _eventAreaRepo.GetByIdAsync(eventSeat.EventAreaId);
            return eventArea.EventId;
        }
    }
}
