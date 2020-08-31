using System.Collections.Generic;
using NodaTime;

namespace Eventshuffle.Domain.Events
{
    public class DateOption
    {
        public DateOption(long eventId, LocalDate date)
        {
            EventId = eventId;
            Date = date;
            Votes = new List<Vote>();
        }

        public long Id { get; }

        public long EventId { get; private set; }

        public LocalDate Date { get; private set; }

        public List<Vote> Votes { get; private set; }
    }
}
