using EventManagementApi.EntitiesDTO;

namespace EventManagementApi.Services.Interfaces
{
    public interface IEventService : IService<EventDto>
    {
        bool IsAnySeatAvailable(EventDto item);
    }
}
