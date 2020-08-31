using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NodaTime;

namespace Eventshuffle.Api.Requests.Events
{
    public class CreateEventRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public IEnumerable<LocalDate> Dates { get; set; }
    }
}
