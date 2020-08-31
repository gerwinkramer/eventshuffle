using System.Collections.Generic;
using NodaTime;

namespace Eventshuffle.Application.Features.Events
{
    public class VoteDto
    {
        public LocalDate Date { get; set; }

        public List<string> People { get; set; }
    }
}
