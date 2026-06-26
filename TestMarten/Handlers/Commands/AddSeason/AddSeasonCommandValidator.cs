using FluentValidation;
using TestMarten.Handlers.Commands.CreateMovie;

namespace TestMarten.Handlers.Commands.AddSeason;

public class AddSeasonCommandValidator : AbstractValidator<AddSeasonCommand>
{
    public AddSeasonCommandValidator()
    { 

        RuleFor(season => season.Name).NotEmpty();
        RuleFor(season => season.MovieId).NotEmpty();

    }
}


 