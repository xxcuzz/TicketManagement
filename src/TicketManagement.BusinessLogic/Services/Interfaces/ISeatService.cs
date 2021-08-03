using System.Collections.Generic;
using TicketManagement.BusinessLogic.EntitiesDTO;

namespace TicketManagement.BusinessLogic.Services.Interfaces
{
    public interface ISeatService : IService<SeatDto>
    {
        IEnumerable<SeatDto> GetSeatsByAreaId(int areaId);
    }
}
