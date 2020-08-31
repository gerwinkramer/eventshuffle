using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Eventshuffle.Application.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Eventshuffle.Application.Features.Events.Queries.GetAllEvents
{
    public class GetEventQueryHandler : IRequestHandler<GetAllEventsQuery, EventListDto>
    {
        private readonly IEventshuffleDbContext _context;

        public GetEventQueryHandler(IEventshuffleDbContext context)
        {
            _context = context;
        }
        
        public async Task<EventListDto> Handle(GetAllEventsQuery request, CancellationToken cancellationToken)
        {
            var events = await _context.Events
                .Select(entity => new EventItemDto {Id = entity.Id, Name = entity.Name})
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return new EventListDto { Events = events };
        }
    }
}
