using System.Collections.Generic;
using System.Linq;
using Eventshuffle.Domain.Events;
using Eventshuffle.Infrastructure.Persistence;
using NodaTime;

namespace Eventshuffle.Api.IntegrationTests
{
    public class SeedData
    {
        private static readonly object _lock = new object();

        public static void Populate(EventshuffleDbContext context)
        {
            lock (_lock)
            {
                if (!context.Events.Any())
                {
                    context.Events.Add(new EventEntity(
                        "Name1",
                        new List<LocalDate>
                        {
                            new LocalDate(2000, 01, 01)
                        }));

                    context.Events.Add(new EventEntity(
                        "Name2", 
                        new List<LocalDate>()));

                    context.SaveChanges();
                }
            }
        }
    }
}
