using System.Threading;
using System.Threading.Tasks;
using Eventshuffle.Application.Services;

namespace Eventshuffle.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EventshuffleDbContext _context;
        
        public UnitOfWork(EventshuffleDbContext context)
        {
            _context = context;
        }

        public async Task<int> Save(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
