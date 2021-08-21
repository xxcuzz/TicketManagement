using System.Collections.Generic;
using System.Threading.Tasks;
using TicketManagement.BusinessLogic.EntitiesDTO;

namespace TicketManagement.BusinessLogic.Services.Interfaces
{
    public interface IUserTicketService : IService<UserTicketDto>
    {
        IEnumerable<UserTicketDto> GetAllTicketsForUser(string userId);

        Task<bool> IsAnyTicketStillAvailable(string id);

        Task DeteleAllTicketsForUser(string id);
    }
}
