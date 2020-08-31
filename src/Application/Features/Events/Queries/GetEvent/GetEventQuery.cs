using MediatR;

namespace Eventshuffle.Application.Features.Events.Queries.GetEvent
{
    public class GetEventQuery : IRequest<EventDto>
    {
        public GetEventQuery(long id)
        {
            Id = id;
        }

        public long Id { get; }
    }
}
