using System.Collections.Generic;
using MediatR;
using NodaTime;

namespace Eventshuffle.Application.Features.Events.Commands.CreateEvent
{
    public class CreateEventCommand : IRequest<EventCreatedDto>
    {
        public CreateEventCommand(string name, IEnumerable<LocalDate> dates)
        {
            Name = name;
            Dates = dates;
        }

        public string Name { get; }

        public IEnumerable<LocalDate> Dates { get; }
    }
}
