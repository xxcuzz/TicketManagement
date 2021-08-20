using TicketManagement.BusinessLogic.EntitiesDTO;

namespace TicketManagement.BusinessLogic.Services.Interfaces
{
    public interface IEventService : IService<EventDto>
    {
        bool IsAnySeatAvailable(EventDto item);

        bool IsAnyEventSeatPurchased(int id);
    }
}
