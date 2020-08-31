using System.Threading;
using System.Threading.Tasks;

namespace Eventshuffle.Domain.Events
{
    public interface IEventRepository
    {
        Task<EventEntity> GetById(long id, CancellationToken cancellationToken);

        Task Add(EventEntity eventEntity);
    }
}
