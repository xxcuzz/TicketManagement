using System;
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
        private readonly IEventSeatService _eventSeatService;
        private readonly IEventAreaService _eventAreaService;
        private readonly IEventService _eventService;

        public UserTicketService(IRepository<UserTicket> userTicketRepo,
            IMapper mapper,
            IEventSeatService eventSeatService,
            IEventAreaService eventAreaService,
            IEventService eventService)
        {
            _userTicketRepo = userTicketRepo;
            _mapper = mapper;
            _eventSeatService = eventSeatService;
            _eventAreaService = eventAreaService;
            _eventService = eventService;
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

        public async Task<bool> IsAnyTicketStillAvailable(string id)
        {
            var userTickets = GetAllTicketsForUser(id);

            foreach (var ticket in userTickets)
            {
                var seat = await _eventSeatService.GetById(ticket.SeatId);
                var area = await _eventAreaService.GetById(seat.EventAreaId);
                var event1 = await _eventService.GetByIdAsync(area.EventId);

                if (event1.EventStart > DateTime.Now)
                {
                    return true;
                }
            }

            return false;
        }

        public async Task DeteleAllTicketsForUser(string id)
        {
            var ticketsForUser = GetAllTicketsForUser(id);

            foreach (var ticket in ticketsForUser)
            {
                await DeleteAsync(ticket);
            }
        }
    }
}
