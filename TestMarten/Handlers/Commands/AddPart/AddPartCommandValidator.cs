using FluentValidation;

namespace TestMarten.Handlers.Commands.AddPart;

public class AddPartCommandValidator : AbstractValidator<AddPartCommand>
{
    public AddPartCommandValidator()
    {
        RuleFor(x => x.MovieId)
            .NotEmpty();

        RuleFor(x => x.SeasonId)
            .NotEmpty();

        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Duration)
            .GreaterThan(0);
    }
}
