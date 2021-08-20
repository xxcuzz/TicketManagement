using TicketManagement.BusinessLogic.EntitiesDTO;

namespace TicketManagement.BusinessLogic.Services.Interfaces
{
    public interface ILayoutService : IService<LayoutDto>
    {
        int GetLayoutIdByDescription(string description);
    }
}
