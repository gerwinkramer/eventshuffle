using System.Collections.Generic;

namespace Eventshuffle.Application.Features.Events.Queries.GetSuitableDates
{
    public class SuitableDatesDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public List<SuitableDateItemDto> Dates { get; set; }
    }
}
