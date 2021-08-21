using System.Collections.Generic;
using System.Threading.Tasks;
using TicketManagement.BusinessLogic.EntitiesDTO;

namespace TicketManagement.BusinessLogic.Services.Interfaces
{
    public interface IEventAreaService
    {
        Task<bool> UpdateEventArea(EventAreaDto item);

        IEnumerable<EventAreaDto> GetAll();

        Task<EventAreaDto> GetById(int id);

        IEnumerable<EventAreaDto> GetAllEventAreasForEvent(int eventId);

        Task<decimal> GetEventAreaPrice(int eventAreaId);
    }
}
