using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TicketManagement.BusinessLogic.EntitiesDTO;
using TicketManagement.BusinessLogic.Services.Interfaces;
using TicketManagement.DataAccess.Entities;
using TicketManagement.DataAccess.InterfacesRepository;

namespace TicketManagement.BusinessLogic.Services
{
    public class UserTicketService : IUserTicketService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<UserTicket> _userTicketRepo;

        public UserTicketService(IRepository<UserTicket> userTicketRepo, IMapper mapper)
        {
            _userTicketRepo = userTicketRepo;
            _mapper = mapper;
        }

        public async Task<bool> CreateAsync(UserTicketDto item)
        {
            if (item == null)
            {
                return false;
            }

            var userTicket = _mapper.Map<UserTicketDto, UserTicket>(item);
            return await _userTicketRepo.AddAsync(userTicket);
        }

        public async Task<bool> DeleteAsync(UserTicketDto item)
        {
            var userTicket = _mapper.Map<UserTicketDto, UserTicket>(item);
            return await _userTicketRepo.DeleteAsync(userTicket);
        }

        public IEnumerable<UserTicketDto> GetAll()
        {
            var userTickets = _userTicketRepo.GetAll().ToList();
            var userTicketDtos = _mapper.Map<List<UserTicket>, List<UserTicketDto>>(userTickets);
            return userTicketDtos;
        }

        public IEnumerable<UserTicketDto> GetAllTicketsForUser(string userId)
        {
            var userTickets = GetTicketsForUser(userId).ToList();
            var userTicketDtos = _mapper.Map<List<UserTicket>, List<UserTicketDto>>(userTickets);
            return userTicketDtos;
        }

        public async Task<UserTicketDto> GetByIdAsync(int id)
        {
            var userTicketItem = await _userTicketRepo.GetByIdAsync(id);
            var userTicketDto = _mapper.Map<UserTicket, UserTicketDto>(userTicketItem);
            return userTicketDto;
        }

        public async Task<bool> UpdateAsync(UserTicketDto item)
        {
            var userTicket = _mapper.Map<UserTicketDto, UserTicket>(item);
            return await _userTicketRepo.UpdateAsync(userTicket);
        }

        public IQueryable<UserTicket> GetTicketsForUser(string userId)
        {
            return _userTicketRepo.GetAll().Where(ticket => ticket.UserId == userId);
        }
    }
}
