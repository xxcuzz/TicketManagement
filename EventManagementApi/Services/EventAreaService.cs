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
    public class EventAreaService : IEventAreaService
    {
        private readonly IRepository<EventArea> _eventAreaRepo;
        private readonly IMapper _mapper;

        public EventAreaService(IRepository<EventArea> eventAreaRepo, IMapper mapper)
        {
            _eventAreaRepo = eventAreaRepo;
            _mapper = mapper;
        }

        public async Task<bool> UpdateEventArea(EventAreaDto item)
        {
            var eventArea = _mapper.Map<EventAreaDto, EventArea>(item);
            return await _eventAreaRepo.UpdateAsync(eventArea);
        }

        public IEnumerable<EventAreaDto> GetAll()
        {
            var eventAreas = _eventAreaRepo.GetAll().ToList();
            var eventAreaDtos = _mapper.Map<List<EventArea>, List<EventAreaDto>>(eventAreas);
            return eventAreaDtos;
        }

        public IEnumerable<EventAreaDto> GetAllEventAreasForEvent(int eventId)
        {
            var eventAreas = GetEventAreasByEventIdAsync(eventId).ToList();
            var eventAreaDtos = _mapper.Map<List<EventArea>, List<EventAreaDto>>(eventAreas);
            return eventAreaDtos;
        }

        public IQueryable<EventArea> GetEventAreasByEventIdAsync(int eventId)
        {
            return _eventAreaRepo.GetAll().Where(ea => ea.EventId == eventId);
        }

        public async Task<decimal> GetEventAreaPrice(int eventAreaId)
        {
            var eventArea = await _eventAreaRepo.GetByIdAsync(eventAreaId);
            return eventArea.Price;
        }
    }
}
