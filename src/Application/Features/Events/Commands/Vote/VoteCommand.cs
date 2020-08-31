using System.Collections.Generic;
using MediatR;
using NodaTime;

namespace Eventshuffle.Application.Features.Events.Commands.Vote
{
    public class VoteCommand : IRequest<EventDto>
    {
        public VoteCommand(long eventId, string name, IEnumerable<LocalDate> votes)
        {
            EventId = eventId;
            Name = name;
            Votes = votes;
        }

        public long EventId { get; }

        public string Name { get; }

        public IEnumerable<LocalDate> Votes { get; }
    }
}
