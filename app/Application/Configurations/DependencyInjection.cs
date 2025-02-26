using Application.Configurations.Mappings;
using Domain.Interfaces;
using Domain.Interfaces.DomainServices;
using Domain.Interfaces.Repositories;
using Domain.Notifications;
using Domain.Services;
using Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ITableRepository, TableRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<IConsumptionRepository, ConsumptionRepository>();

            return services;
        }

        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<ITableService, TableService>();
            services.AddScoped<IPersonService, PersonService>();

            return services;
        }

        public static IServiceCollection AddNotificationsServices(this IServiceCollection services)
        {
            services.AddScoped<INotificationContext, NotificationContext>();

            return services;
        }

        public static IServiceCollection AddMapperProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));

            return services;
        }

        public static IServiceCollection RegisterRequestHandler(this IServiceCollection services)
        {
            return services.AddMediatR(cf => cf.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
        }
    }
}