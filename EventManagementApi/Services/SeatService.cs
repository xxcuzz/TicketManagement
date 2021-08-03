using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventManagementApi.EntitiesDTO;
using EventManagementApi.Services.Interfaces;
using EventManagementApi.Validation;
using TicketManagement.DataAccess.Entities;
using TicketManagement.DataAccess.InterfacesRepository;

namespace EventManagementApi.Services
{
    public class SeatService : ISeatService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Seat> _seatRepo;
        private readonly IRepository<Area> _areaRepo;

        public SeatService(IRepository<Seat> seatRepo, IRepository<Area> areaRepo, IMapper mapper)
        {
            _seatRepo = seatRepo;
            _areaRepo = areaRepo;
            _mapper = mapper;
        }

        public async Task<bool> CreateAsync(SeatDto item)
        {
            if (item == null)
            {
                return false;
            }

            var seat = _mapper.Map<SeatDto, Seat>(item);

            var area = await _areaRepo.GetByIdAsync(item.AreaId);
            var seats = GetSeatsByAreaId(item.AreaId);

            return SeatValidator.Validate(seat, seats, area) && await _seatRepo.AddAsync(seat);
        }

        public async Task<bool> UpdateAsync(SeatDto item)
        {
            var seat = _mapper.Map<SeatDto, Seat>(item);
            return await _seatRepo.UpdateAsync(seat);
        }

        public async Task<bool> DeleteAsync(SeatDto item)
        {
            var seat = _mapper.Map<SeatDto, Seat>(item);
            return await _seatRepo.DeleteAsync(seat);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var seat = await _seatRepo.GetByIdAsync(id);
            return await _seatRepo.DeleteAsync(seat);
        }

        public IEnumerable<SeatDto> GetAll()
        {
            var seats = _seatRepo.GetAll().ToList();
            var seatDtos = _mapper.Map<List<Seat>, List<SeatDto>>(seats);
            return seatDtos;
        }

        public async Task<SeatDto> GetByIdAsync(int id)
        {
            var seatItem = await _seatRepo.GetByIdAsync(id);
            var seatDto = _mapper.Map<Seat, SeatDto>(seatItem);
            return seatDto;
        }

        public IEnumerable<SeatDto> GetSeatsByAreaId(int areaId)
        {
            var seats = _seatRepo.GetAll().Where(seat => seat.AreaId == areaId).ToList();
            var seatDtos = _mapper.Map<List<Seat>, List<SeatDto>>(seats);
            return seatDtos;
        }
    }
}
