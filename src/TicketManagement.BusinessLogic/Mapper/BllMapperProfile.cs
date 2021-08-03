using AutoMapper;
using TicketManagement.BusinessLogic.EntitiesDTO;
using TicketManagement.DataAccess.Entities;

namespace TicketManagement.BusinessLogic.Mapper
{
    public class BllMapperProfile : Profile
    {
        public BllMapperProfile()
        {
            CreateMap<Event, EventDto>().ReverseMap();
            CreateMap<Area, AreaDto>().ReverseMap();
            CreateMap<Layout, LayoutDto>().ReverseMap();
            CreateMap<Seat, SeatDto>().ReverseMap();
            CreateMap<Venue, VenueDto>().ReverseMap();
            CreateMap<EventSeat, EventSeatDto>().ReverseMap();
            CreateMap<EventArea, EventAreaDto>().ReverseMap();
            CreateMap<UserTicket, UserTicketDto>().ReverseMap();

            CreateMap<ThirdPartyEventDto, EventDto>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.PosterImage))
                .ForMember(dest => dest.EventStart, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.EventEnd, opt => opt.MapFrom(src => src.EndDate));
        }
    }
}