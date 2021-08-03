using System.Collections.Generic;
using TicketManagement.BusinessLogic.EntitiesDTO;

namespace TicketManagement.BusinessLogic.Services.Interfaces
{
    public interface IUserTicketService : IService<UserTicketDto>
    {
        IEnumerable<UserTicketDto> GetAllTicketsForUser(string userId);
    }
}
