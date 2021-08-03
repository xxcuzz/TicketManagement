using System.Collections.Generic;
using System.Threading.Tasks;

namespace TicketManagement.BusinessLogic.Services
{
    public interface IPurchaseService
    {
        Task<bool> BuyTicket(string userId, int seatId);

        Task<bool> CreateUserTicket(string userId, int seatId);

        Task<bool> ChangeSeatState(int seatId);

        Task<decimal> GetFullPrice(IEnumerable<int> eventSeatIds);
    }
}
