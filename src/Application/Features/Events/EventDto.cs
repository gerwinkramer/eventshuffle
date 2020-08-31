using System.Collections.Generic;
using NodaTime;

namespace Eventshuffle.Application.Features.Events
{
    public class EventDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public List<LocalDate> Dates { get; set; }

        public List<VoteDto> Votes { get; set; }
    }
}
