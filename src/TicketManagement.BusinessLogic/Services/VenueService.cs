using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TicketManagement.BusinessLogic.EntitiesDTO;
using TicketManagement.BusinessLogic.Services.Interfaces;
using TicketManagement.BusinessLogic.Validation;
using TicketManagement.DataAccess.Entities;
using TicketManagement.DataAccess.InterfacesRepository;

namespace TicketManagement.BusinessLogic.Services
{
    public class VenueService : IService<VenueDto>
    {
        private readonly IRepository<Venue> _venueRepo;
        private readonly IMapper _mapper;

        public VenueService(IRepository<Venue> venueRepo, IMapper mapper)
        {
            _venueRepo = venueRepo;
            _mapper = mapper;
        }

        public async Task<bool> CreateAsync(VenueDto item)
        {
            if (item == null)
            {
                return false;
            }

            var venue = _mapper.Map<VenueDto, Venue>(item);
            var venues = GetVenuesByDescription(item.Description);
            return VenueValidator.Validate(item, venues) && await _venueRepo.AddAsync(venue);
        }

        public async Task<bool> UpdateAsync(VenueDto item)
        {
            var venue = _mapper.Map<VenueDto, Venue>(item);
            return await _venueRepo.UpdateAsync(venue);
        }

        public async Task<bool> DeleteAsync(VenueDto item)
        {
            var venue = _mapper.Map<VenueDto, Venue>(item);
            return await _venueRepo.DeleteAsync(venue);
        }

        public IEnumerable<VenueDto> GetAll()
        {
            var venues = _venueRepo.GetAll().ToList();
            var venueDtos = _mapper.Map<List<Venue>, List<VenueDto>>(venues);
            return venueDtos;
        }

        public async Task<VenueDto> GetByIdAsync(int id)
        {
            var venueItem = await _venueRepo.GetByIdAsync(id);
            var venueDto = _mapper.Map<Venue, VenueDto>(venueItem);
            return venueDto;
        }

        public IQueryable<Venue> GetVenuesByDescription(string description)
        {
            return _venueRepo.GetAll().Where(d => d.Description == description);
        }
    }
}
