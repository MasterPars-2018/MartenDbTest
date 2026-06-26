using FluentValidation;

namespace TestMarten.Handlers.Commands.UpdateMovie;

public class UpdateMovieCommandValidator : AbstractValidator<UpdateMovieCommand>
{
    public UpdateMovieCommandValidator()
    {


        RuleFor(movie => movie.Name).NotEmpty();
        RuleFor(movie => movie.Description).NotEmpty();
        RuleFor(movie => movie.PublishedAt).GreaterThan(1900);
        RuleFor(movie => movie.Type).NotNull();

    }
}
