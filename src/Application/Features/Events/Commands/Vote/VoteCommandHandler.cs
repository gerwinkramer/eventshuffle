using System.Threading;
using System.Threading.Tasks;
using Eventshuffle.Application.Exceptions;
using Eventshuffle.Application.Features.Events.Mappers;
using Eventshuffle.Application.Services;
using Eventshuffle.Domain.Events;
using MediatR;

namespace Eventshuffle.Application.Features.Events.Commands.Vote
{
    public class VoteCommandHandler : IRequestHandler<VoteCommand, EventDto>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IUnitOfWork _unitOfWork;

        public VoteCommandHandler(IEventRepository eventRepository, IUnitOfWork unitOfWork)
        {
            _eventRepository = eventRepository;
            _unitOfWork = unitOfWork;
        }
        
        public async Task<EventDto> Handle(VoteCommand request, CancellationToken cancellationToken)
        {
            var eventEntity = await _eventRepository.GetById(request.EventId, cancellationToken);

            if (eventEntity == null)
            {
                throw new NotFoundException($"Event with id {request.EventId} was not found.");
            }

            eventEntity.Vote(request.Name, request.Votes);
            await _unitOfWork.Save(cancellationToken);

            return EventMapper.MapToDto(eventEntity);
        }
    }
}
