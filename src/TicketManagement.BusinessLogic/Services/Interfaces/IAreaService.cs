using System.Collections.Generic;
using TicketManagement.BusinessLogic.EntitiesDTO;

namespace TicketManagement.BusinessLogic.Services.Interfaces
{
    public interface IAreaService : IService<AreaDto>
    {
        IEnumerable<AreaDto> GetAreasByLayoutId(int layoutId);
    }
}
