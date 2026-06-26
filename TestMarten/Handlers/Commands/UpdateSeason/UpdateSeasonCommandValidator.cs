using FluentValidation;

namespace TestMarten.Handlers.Commands.UpdateSeason;

public class UpdateSeasonCommandValidator : AbstractValidator<UpdateSeasonCommand>
{
    public UpdateSeasonCommandValidator()
    {
        RuleFor(x => x.MovieId)
            .NotEmpty();

        RuleFor(x => x.SeasonId)
            .NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(200);
    }
}