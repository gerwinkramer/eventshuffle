using System.Threading;
using System.Threading.Tasks;
using Eventshuffle.Application.Features.Events.Mappers;
using Eventshuffle.Domain.Events;
using MediatR;

namespace Eventshuffle.Application.Features.Events.Queries.GetEvent
{
    public class GetEventQueryHandler : IRequestHandler<GetEventQuery, EventDto>
    {
        private readonly IEventRepository _eventRepository;

        public GetEventQueryHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }
        
        public async Task<EventDto> Handle(GetEventQuery request, CancellationToken cancellationToken)
        {
            var eventEntity = await _eventRepository.GetById(request.Id, cancellationToken);

            return EventMapper.MapToDto(eventEntity);
        }
    }
}
