using System.Collections.Generic;
using System.Threading.Tasks;
using TicketManagement.BusinessLogic.EntitiesDTO;

namespace TicketManagement.BusinessLogic.Services.Interfaces
{
    public interface IEventSeatService
    {
        Task<bool> ChangeEventSeatState(EventSeatDto item);

        IEnumerable<EventSeatDto> GetAll();

        Task<EventSeatDto> GetById(int id);

        IEnumerable<EventSeatDto> GetAllEventSeatsForEventArea(int eventAreaId);

        IEnumerable<EventSeatDto> GetAllEventSeatsForEvent(int eventId);

        Task<decimal> GetPriceForEventSeat(int id);

        Task<string> GetDescriptionOfCurruntEvent(int id);
    }
}
