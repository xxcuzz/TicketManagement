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
    public class AreaService : IAreaService
    {
        private readonly IRepository<Area> _areaRepo;
        private readonly IRepository<Layout> _layoutRepo;
        private readonly IMapper _mapper;

        public AreaService(IRepository<Area> areaRepo, IRepository<Layout> layoutRepo, IMapper mapper)
        {
            _areaRepo = areaRepo;
            _layoutRepo = layoutRepo;
            _mapper = mapper;
        }

        public async Task<bool> CreateAsync(AreaDto item)
        {
            if (item == null)
            {
                return false;
            }

            var area = _mapper.Map<AreaDto, Area>(item);

            var layout = await _layoutRepo.GetByIdAsync(item.LayoutId);
            var areas = GetAreasByLayoutId(item.LayoutId);

            return AreaValidator.Validate(area, layout, areas) && await _areaRepo.AddAsync(area);
        }

        public async Task<bool> UpdateAsync(AreaDto item)
        {
            var area = _mapper.Map<AreaDto, Area>(item);
            return await _areaRepo.UpdateAsync(area);
        }

        public async Task<bool> DeleteAsync(AreaDto item)
        {
            var area = _mapper.Map<AreaDto, Area>(item);
            return await _areaRepo.DeleteAsync(area);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var area = await _areaRepo.GetByIdAsync(id);
            return await _areaRepo.DeleteAsync(area);
        }

        public IEnumerable<AreaDto> GetAll()
        {
            var areas = _areaRepo.GetAll().ToList();
            var areaDtos = _mapper.Map<List<Area>, List<AreaDto>>(areas);
            return areaDtos;
        }

        public async Task<AreaDto> GetByIdAsync(int id)
        {
            var areaItem = await _areaRepo.GetByIdAsync(id);
            var areaDto = _mapper.Map<Area, AreaDto>(areaItem);
            return areaDto;
        }

        public IEnumerable<AreaDto> GetAreasByLayoutId(int layoutId)
        {
            var areas = _areaRepo.GetAll().Where(area => area.LayoutId == layoutId).ToList();
            var areaDtos = _mapper.Map<List<Area>, List<AreaDto>>(areas);
            return areaDtos;
        }
    }
}
