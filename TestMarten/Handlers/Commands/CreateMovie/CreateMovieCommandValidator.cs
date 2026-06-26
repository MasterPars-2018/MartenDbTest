using FluentValidation;

namespace TestMarten.Handlers.Commands.CreateMovie;

public class CreateMovieCommandValidator : AbstractValidator<CreateMoveCommand>
{
    public CreateMovieCommandValidator()
    {


        RuleFor(movie => movie.Name).NotEmpty();
        RuleFor(movie => movie.Description).NotEmpty();
        RuleFor(movie => movie.PublishedAt).GreaterThan(1900);
        RuleFor(movie => movie.Type).NotNull();

    }
}
