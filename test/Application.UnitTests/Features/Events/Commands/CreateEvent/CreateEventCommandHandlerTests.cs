using System.Linq;
using System.Threading;
using Eventshuffle.Application.Features.Events.Commands.CreateEvent;
using Eventshuffle.Application.Services;
using Eventshuffle.Domain.Events;
using NodaTime;
using NSubstitute;
using Shouldly;
using Xunit;

namespace Eventshuffle.Application.UnitTests.Features.Events.Commands.CreateEvent
{
    public class CreateEventCommandHandlerTests
    {
        private readonly IEventRepository _eventRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly CreateEventCommandHandler _handler;

        public CreateEventCommandHandlerTests()
        {
            _eventRepository = Substitute.For<IEventRepository>();
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _handler = new CreateEventCommandHandler(_eventRepository, _unitOfWork);
        }
        
        [Fact]
        public void Handle_ShouldCreateEvent()
        {
            var name = "Name";
            var dates = Enumerable.Empty<LocalDate>();
            var command = new CreateEventCommand(name, dates);

            var result = _handler.Handle(command, new CancellationToken());

            _eventRepository.Received(1).Add(Arg.Is<EventEntity>(e => e.Name == name));
            _unitOfWork.Received(1).Save(Arg.Any<CancellationToken>());
        }
        
        [Fact]
        public void Handle_ShouldReturnDto()
        {
            var name = "Name";
            var dates = Enumerable.Empty<LocalDate>();
            var command = new CreateEventCommand(name, dates);

            var result = _handler.Handle(command, new CancellationToken());

            result.Result.ShouldBeOfType<EventCreatedDto>();
        }
    }
}
