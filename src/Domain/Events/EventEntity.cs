using System;
using System.Collections.Generic;
using System.Linq;
using NodaTime;

namespace Eventshuffle.Domain.Events
{
    public class EventEntity
    {
        private readonly List<DateOption> _dateOptions;

        public EventEntity()
        {
            _dateOptions = new List<DateOption>();
        }
        
        public EventEntity(string name, IEnumerable<LocalDate> dates)
        {
            Name = name;
            _dateOptions = dates
                .Distinct()
                .Select(date => new DateOption(Id, date))
                .ToList();
        }

        public long Id { get; }

        public string Name { get; private set; }

        public IReadOnlyCollection<DateOption> DateOptions => _dateOptions.AsReadOnly();

        public void Vote(string name, IEnumerable<LocalDate> dates)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name is required.");
            }

            if (dates == null)
            {
                throw new ArgumentNullException(nameof(dates));
            }

            foreach (var dateOption in DateOptions)
            {
                // Idempotent action, remove old votes first.
                dateOption.Votes.RemoveAll(vote => vote.Name == name);
            }

            foreach (var date in dates)
            {
                var dateOption = DateOptions.FirstOrDefault(option => option.Date == date);
                if (dateOption == null)
                {
                    // Voted on a non-existing date, for now we just ignore this one.
                    continue;
                }

                var vote = new Vote(Id, name, dateOption.Id);
                dateOption.Votes.Add(vote);
            }
        }

        public IEnumerable<DateOption> GetSuitableDateOptionsForAllVoters()
        {
            var allVotersCount = DateOptions
                .SelectMany(dateOption => dateOption.Votes)
                .Select(vote => vote.Name)
                .Distinct()
                .Count();

            if (allVotersCount == 0)
            {
                return Enumerable.Empty<DateOption>();
            }

            return DateOptions
                .Where(dateOption => dateOption.Votes.Count == allVotersCount);
        }
    }
}
