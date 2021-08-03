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
    public class LayoutService : IService<LayoutDto>
    {
        private readonly IRepository<Layout> _layoutRepo;
        private readonly IRepository<Venue> _venueRepo;
        private readonly IMapper _mapper;

        public LayoutService(IRepository<Layout> layoutRepo, IRepository<Venue> venueRepo, IMapper mapper)
        {
            _layoutRepo = layoutRepo;
            _venueRepo = venueRepo;
            _mapper = mapper;
        }

        public async Task<bool> CreateAsync(LayoutDto item)
        {
            if (item == null)
            {
                return false;
            }

            var layout = _mapper.Map<LayoutDto, Layout>(item);

            var venue = await _venueRepo.GetByIdAsync(item.VenueId);
            var layouts = GetLayoutsByVenueId(item.Id);

            return LayoutValidator.Validate(layout, venue, layouts) && await _layoutRepo.AddAsync(layout);
        }

        public async Task<bool> UpdateAsync(LayoutDto item)
        {
            var layout = _mapper.Map<LayoutDto, Layout>(item);
            return await _layoutRepo.UpdateAsync(layout);
        }

        public async Task<bool> DeleteAsync(LayoutDto item)
        {
            var layout = _mapper.Map<LayoutDto, Layout>(item);
            return await _layoutRepo.DeleteAsync(layout);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var layout = await _layoutRepo.GetByIdAsync(id);
            return await _layoutRepo.DeleteAsync(layout);
        }

        public IEnumerable<LayoutDto> GetAll()
        {
            var layouts = _layoutRepo.GetAll().ToList();
            var layoutDtos = _mapper.Map<List<Layout>, List<LayoutDto>>(layouts);
            return layoutDtos;
        }

        public async Task<LayoutDto> GetByIdAsync(int id)
        {
            var layoutItem = await _layoutRepo.GetByIdAsync(id);
            var layoutDto = _mapper.Map<Layout, LayoutDto>(layoutItem);
            return layoutDto;
        }

        public IQueryable<Layout> GetLayoutsByVenueId(int venueId)
        {
            return _layoutRepo.GetAll().Where(l => l.VenueId == venueId);
        }
    }
}
