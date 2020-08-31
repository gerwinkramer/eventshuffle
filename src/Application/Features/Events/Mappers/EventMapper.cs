using System.Linq;
using Eventshuffle.Domain.Events;

namespace Eventshuffle.Application.Features.Events.Mappers
{
    public static class EventMapper
    {
        public static EventDto MapToDto(EventEntity entity)
        {
            var dto = new EventDto
            {
                Id = entity.Id,
                Name = entity.Name
            };

            dto.Dates = entity.DateOptions
                .OrderBy(dateOption => dateOption.Date)
                .Select(dateOption => dateOption.Date).ToList();

            dto.Votes = entity.DateOptions
                .Where(dateOption => dateOption.Votes.Any())
                .OrderBy(dateOption => dateOption.Date)
                .Select(option => new VoteDto
                {
                    Date = option.Date,
                    People = option.Votes.Select(vote => vote.Name).ToList()
                })
                .ToList();

            return dto;
        }
    }
}
