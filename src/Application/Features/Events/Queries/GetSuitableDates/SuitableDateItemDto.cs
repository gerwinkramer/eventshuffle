using System.Collections.Generic;
using NodaTime;

namespace Eventshuffle.Application.Features.Events.Queries.GetSuitableDates
{
    public class SuitableDateItemDto
    {
        public LocalDate Date { get; set; }

        public List<string> People { get; set; }
    }
}
