using AutoMapper;
using TicketManagement.BusinessLogic.EntitiesDTO;
using Web.ViewModel;

namespace Web.Models
{
    public class WebMapperProfile : Profile
    {
        public WebMapperProfile()
        {
            CreateMap<EditEventViewModel, EventDto>().ReverseMap();
        }
    }
}
