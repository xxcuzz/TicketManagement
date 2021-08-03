using System.Collections.Generic;
using System.Threading.Tasks;
using EventManagementApi.EntitiesDTO;

namespace EventManagementApi.Services.Interfaces
{
    public interface IEventAreaService
    {
        Task<bool> UpdateEventArea(EventAreaDto item);

        IEnumerable<EventAreaDto> GetAll();

        IEnumerable<EventAreaDto> GetAllEventAreasForEvent(int eventId);

        Task<decimal> GetEventAreaPrice(int eventAreaId);
    }
}
