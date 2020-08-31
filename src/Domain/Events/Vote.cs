namespace Eventshuffle.Domain.Events
{
    public class Vote
    {
        public Vote(long eventId, string name, long dateOptionId)
        {
            EventId = eventId;
            Name = name;
            DateOptionId = dateOptionId;
        }
        
        public long Id { get; }

        public long EventId { get; private set; }

        public string Name { get; private set; }

        public long DateOptionId { get; private set; }
    }
}
