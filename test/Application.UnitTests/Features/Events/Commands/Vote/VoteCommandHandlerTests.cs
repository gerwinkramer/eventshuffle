using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Eventshuffle.Application.Exceptions;
using Eventshuffle.Application.Features.Events.Commands.Vote;
using Eventshuffle.Application.Services;
using Eventshuffle.Domain.Events;
using NodaTime;
using NSubstitute;
using Shouldly;
using Xunit;

namespace Eventshuffle.Application.UnitTests.Features.Events.Commands.Vote
{
    public class VoteCommandHandlerTests
    {
        private readonly IEventRepository _eventRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly VoteCommandHandler _handler;

        public VoteCommandHandlerTests()
        {
            _eventRepository = Substitute.For<IEventRepository>();
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _handler = new VoteCommandHandler(_eventRepository, _unitOfWork);
        }

        [Fact]
        public void Handle_ShouldVote()
        {
            var eventId = 1L;
            var name = "Name";
            var date = new LocalDate(2000, 01, 01);
            var dates = new List<LocalDate> { date };
            var command = new VoteCommand(eventId, name, dates);
            _eventRepository.GetById(eventId, new CancellationToken())
                .Returns(new EventEntity(name, dates));

            var result = _handler.Handle(command, new CancellationToken());

            result.Result.Votes.First().Date.ShouldBe(date);
            result.Result.Votes.First().People.First().ShouldBe(name);
        }

        [Fact]
        public void Handle_ShouldThrowException_WhenEventIsNotFound()
        {
            var eventId = 1L;
            var name = "Name";
            var date = new LocalDate(2000, 01, 01);
            var dates = new List<LocalDate> { date };
            var command = new VoteCommand(eventId, name, dates);
            _eventRepository
                .GetById(eventId, new CancellationToken())
                .Returns((EventEntity)null);

            Should.Throw<NotFoundException>(() => _handler.Handle(command, new CancellationToken()));
        }

        [Fact]
        public void Handle_ShouldSaveVote()
        {
            var eventId = 1L;
            var name = "Name";
            var date = new LocalDate(2000, 01, 01);
            var dates = new List<LocalDate> { date };
            var command = new VoteCommand(eventId, name, dates);
            _eventRepository
                .GetById(eventId, new CancellationToken())
                .Returns(new EventEntity(name, dates));

            var result = _handler.Handle(command, new CancellationToken());

            _unitOfWork.Received(1).Save(Arg.Any<CancellationToken>());
        }
    }
}
