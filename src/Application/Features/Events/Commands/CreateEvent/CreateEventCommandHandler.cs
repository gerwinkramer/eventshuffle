using System.Threading;
using System.Threading.Tasks;
using Eventshuffle.Application.Services;
using Eventshuffle.Domain.Events;
using MediatR;

namespace Eventshuffle.Application.Features.Events.Commands.CreateEvent
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, EventCreatedDto>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateEventCommandHandler(IEventRepository eventRepository, IUnitOfWork unitOfWork)
        {
            _eventRepository = eventRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<EventCreatedDto> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var eventEntity = new EventEntity(request.Name, request.Dates);

            await _eventRepository.Add(eventEntity);
            await _unitOfWork.Save(cancellationToken);

            return new EventCreatedDto { Id = eventEntity.Id };
        }
    }
}
