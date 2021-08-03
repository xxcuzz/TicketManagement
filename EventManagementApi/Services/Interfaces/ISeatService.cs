using System.Collections.Generic;
using EventManagementApi.EntitiesDTO;

namespace EventManagementApi.Services.Interfaces
{
    public interface ISeatService : IService<SeatDto>
    {
        IEnumerable<SeatDto> GetSeatsByAreaId(int areaId);
    }
}
