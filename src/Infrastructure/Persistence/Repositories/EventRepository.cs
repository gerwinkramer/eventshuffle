using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Eventshuffle.Domain.Events;
using Microsoft.EntityFrameworkCore;

namespace Eventshuffle.Infrastructure.Persistence.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly EventshuffleDbContext _context;
        
        public EventRepository(EventshuffleDbContext context)
        {
            _context = context;
        }
        
        public async Task<EventEntity> GetById(long id, CancellationToken cancellationToken)
        {
            return await _context.Events
                .Where(entity => entity.Id == id)
                .Include(entity => entity.DateOptions)
                .ThenInclude(dateOption => dateOption.Votes)
                .SingleOrDefaultAsync(cancellationToken);
        }

        public async Task Add(EventEntity eventEntity)
        {
            await _context.Events.AddAsync(eventEntity);
        }
    }
}
