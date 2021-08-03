using System.Collections.Generic;
using EventManagementApi.EntitiesDTO;

namespace EventManagementApi.Services.Interfaces
{
    public interface IAreaService : IService<AreaDto>
    {
        IEnumerable<AreaDto> GetAreasByLayoutId(int layoutId);
    }
}
