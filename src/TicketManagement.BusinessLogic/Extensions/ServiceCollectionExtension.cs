using Microsoft.Extensions.DependencyInjection;
using TicketManagement.BusinessLogic.EntitiesDTO;
using TicketManagement.BusinessLogic.Services;
using TicketManagement.BusinessLogic.Services.Interfaces;
using TicketManagement.DataAccess.EFRepositories;
using TicketManagement.DataAccess.Entities;
using TicketManagement.DataAccess.InterfacesRepository;

namespace TicketManagement.BusinessLogic.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddBllServices(this IServiceCollection services)
        {
            services.AddScoped<IAreaService, AreaService>();
            services.AddScoped<IEventAreaService, EventAreaService>();
            services.AddScoped<IEventSeatService, EventSeatService>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IPurchaseService, PurchaseService>();
            services.AddScoped<ISeatService, SeatService>();
            services.AddScoped<IUserTicketService, UserTicketService>();
            services.AddScoped<ILayoutService, LayoutService>();
            services.AddScoped<IService<VenueDto>, VenueService>();

            return services;
        }

        public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IRepository<Event>, EventEfRepository>();

            return services;
        }
    }
}
