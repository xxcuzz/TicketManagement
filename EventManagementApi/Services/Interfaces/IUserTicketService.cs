using System.Collections.Generic;
using EventManagementApi.EntitiesDTO;

namespace EventManagementApi.Services.Interfaces
{
    public interface IUserTicketService : IService<UserTicketDto>
    {
        IEnumerable<UserTicketDto> GetAllTicketsForUser(string userId);
    }
}
