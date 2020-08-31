using MediatR;

namespace Eventshuffle.Application.Features.Events.Queries.GetAllEvents
{
    public class GetAllEventsQuery : IRequest<EventListDto>
    {
    }
}
