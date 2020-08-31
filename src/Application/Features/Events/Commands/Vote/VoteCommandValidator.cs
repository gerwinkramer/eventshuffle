using FluentValidation;

namespace Eventshuffle.Application.Features.Events.Commands.Vote
{
    public class VoteCommandValidator : AbstractValidator<VoteCommand>
    {
        public VoteCommandValidator()
        {
            RuleFor(c => c.Name)
                .MaximumLength(200)
                .NotEmpty();

            RuleForEach(c => c.Votes)
                .NotEmpty();
        }
    }
}