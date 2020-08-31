using Eventshuffle.Application.Services;
using Eventshuffle.Domain.Events;
using Eventshuffle.Infrastructure.Persistence;
using Eventshuffle.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.NodaTime.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Eventshuffle.Api.Extensions
{
    public static class PersistenceExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EventshuffleDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString(nameof(EventshuffleDbContext)),
                    config =>
                    {
                        config.MigrationsAssembly(typeof(EventshuffleDbContext).Assembly.ToString());
                        config.UseNodaTime();
                    }));

            services.AddScoped<IEventshuffleDbContext>(provider => provider.GetRequiredService<EventshuffleDbContext>());
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
