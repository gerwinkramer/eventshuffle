using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Eventshuffle.Application.Exceptions;
using Eventshuffle.Domain.Events;
using MediatR;

namespace Eventshuffle.Application.Features.Events.Queries.GetSuitableDates
{
    public class GetSuitableDatesQueryHandler : IRequestHandler<GetSuitableDatesQuery, SuitableDatesDto>
    {
        private readonly IEventRepository _eventRepository;

        public GetSuitableDatesQueryHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<SuitableDatesDto> Handle(GetSuitableDatesQuery request, CancellationToken cancellationToken)
        {
            var eventEntity = await _eventRepository.GetById(request.Id, cancellationToken);

            if (eventEntity == null)
            {
                throw new NotFoundException($"Event with id {request.Id} was not found.");
            }

            var suitableDateItemDtos = eventEntity.GetSuitableDateOptionsForAllVoters()
                .Select(dateOption => new SuitableDateItemDto 
                {
                    Date = dateOption.Date,
                    People = dateOption.Votes.Select(vote => vote.Name).ToList()
                })
                .ToList();

            return new SuitableDatesDto
            {
                Id = eventEntity.Id,
                Name = eventEntity.Name,
                Dates = suitableDateItemDtos
            };
        }
    }
}
