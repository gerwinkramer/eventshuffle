using MediatR;

namespace Eventshuffle.Application.Features.Events.Queries.GetSuitableDates
{
    public class GetSuitableDatesQuery : IRequest<SuitableDatesDto>
    {
        public GetSuitableDatesQuery(long id)
        {
            Id = id;
        }

        public long Id { get; }
    }
}
