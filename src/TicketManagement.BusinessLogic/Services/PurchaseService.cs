using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketManagement.BusinessLogic.EntitiesDTO;
using TicketManagement.BusinessLogic.Services.Interfaces;
using TicketManagement.DataAccess.Entities;
using TicketManagement.DataAccess.InterfacesRepository;

namespace TicketManagement.BusinessLogic.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IRepository<EventSeat> _eventSeatRepo;
        private readonly IRepository<EventArea> _eventAreaRepo;
        private readonly IUserTicketService _userTicketService;

        public PurchaseService(
            IUserTicketService userTicketService,
            IRepository<EventArea> eventAreaRepo,
            IRepository<EventSeat> eventSeatRepo)
        {
            _eventSeatRepo = eventSeatRepo;
            _eventAreaRepo = eventAreaRepo;
            _userTicketService = userTicketService;
        }

        public async Task<bool> BuyTicket(string userId, int seatId)
        {
            if (!(await ChangeSeatState(seatId)))
            {
                return false;
            }

            if (!(await CreateUserTicket(userId, seatId)))
            {
                return false;
            }

            return true;
        }

        public async Task<bool> CreateUserTicket(string userId, int seatId)
        {
            var item = new UserTicketDto
            {
                UserId = userId,
                SeatId = seatId,
                PurchaseTime = DateTime.Now,
            };

            return await _userTicketService.CreateAsync(item);
        }

        public async Task<decimal> GetFullPrice(IEnumerable<int> eventSeatIds)
        {
            var price = 0.0M;

            foreach (var eventSeatId in eventSeatIds)
            {
                var eventSeat = await _eventSeatRepo.GetByIdAsync(eventSeatId);
                var eventAreaId = eventSeat.EventAreaId;
                var eventArea = await _eventAreaRepo.GetByIdAsync(eventAreaId);
                price += eventArea.Price;
            }

            return price;
        }

        public async Task<bool> ChangeSeatState(int seatId)
        {
            var eventSeat = await _eventSeatRepo.GetByIdAsync(seatId);
            eventSeat.State = eventSeat.State == 0 ? 1 : 0;
            return await _eventSeatRepo.UpdateAsync(eventSeat);
        }
    }
}
