using System.Linq;
using Eventshuffle.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Eventshuffle.Api.IntegrationTests
{
    public class CustomWebApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup: class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.Remove(services
                    .SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<EventshuffleDbContext>)));

                services.AddDbContext<EventshuffleDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForIntegrationTesting");
                });

                var serviceProvider = services.BuildServiceProvider();

                using (var scope = serviceProvider.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var context = scopedServices.GetRequiredService<EventshuffleDbContext>();
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();
                    SeedData.Populate(context);
                }
            });
        }
    }
}
