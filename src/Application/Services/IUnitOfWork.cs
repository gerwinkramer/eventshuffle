using System.Threading;
using System.Threading.Tasks;

namespace Eventshuffle.Application.Services
{
    public interface IUnitOfWork
    {
        Task<int> Save(CancellationToken cancellationToken);
    }
}