using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NodaTime;

namespace Eventshuffle.Api.Requests.Events
{
    public class VoteRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public List<LocalDate> Votes { get; set; }
    }
}
