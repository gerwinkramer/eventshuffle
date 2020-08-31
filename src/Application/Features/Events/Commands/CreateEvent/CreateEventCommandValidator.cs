using FluentValidation;

namespace Eventshuffle.Application.Features.Events.Commands.CreateEvent
{
    public class CreateEventCommandValidator : AbstractValidator<CreateEventCommand>
    {
        public CreateEventCommandValidator()
        {
            RuleFor(c => c.Name)
                .MaximumLength(200)
                .NotEmpty();

            RuleForEach(c => c.Dates)
                .NotEmpty();
        }
    }
}