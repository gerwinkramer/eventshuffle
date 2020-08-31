using Eventshuffle.Domain.Events;
using Microsoft.EntityFrameworkCore;

namespace Eventshuffle.Application.Services
{
    public interface IEventshuffleDbContext
    {
        public DbSet<EventEntity> Events { get; set; }
        
        public DbSet<Vote> Votes { get; set; }
    }
}
