using Eventshuffle.Application.Services;
using Eventshuffle.Domain.Events;
using Eventshuffle.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Eventshuffle.Infrastructure.Persistence
{
    public class EventshuffleDbContext : DbContext, IEventshuffleDbContext
    {
        public EventshuffleDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<EventEntity> Events { get; set; }

        public DbSet<DateOption> DateOptions { get; set; }

        public DbSet<Vote> Votes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EventConfiguration());
            modelBuilder.ApplyConfiguration(new DateOptionConfiguration());
            modelBuilder.ApplyConfiguration(new VoteConfiguration());
        }
    }
}
