using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.SqlServer.NodaTime.Extensions;
using Microsoft.Extensions.Configuration;

namespace Eventshuffle.Infrastructure.Persistence
{
    public class EventshuffleDbContextFactory : IDesignTimeDbContextFactory<EventshuffleDbContext>
    {
        public EventshuffleDbContext CreateDbContext(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Api"))
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .AddEnvironmentVariables()
                .AddUserSecrets<EventshuffleDbContext>()
                .Build();

            var connectionString = configuration.GetConnectionString(nameof(EventshuffleDbContext));
            
            var optionsBuilder = new DbContextOptionsBuilder<EventshuffleDbContext>();
            optionsBuilder.UseSqlServer(connectionString, config => config.UseNodaTime());

            return new EventshuffleDbContext(optionsBuilder.Options);
        }
    }
}
