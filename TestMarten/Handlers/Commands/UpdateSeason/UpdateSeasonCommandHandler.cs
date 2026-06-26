using Marten;
using TestMarten.Models;
using TestMarten.Models.Events;

namespace TestMarten.Handlers.Commands.UpdateSeason;

public class UpdateSeasonCommandHandler
{
    private readonly IDocumentStore _store;

    public UpdateSeasonCommandHandler(IDocumentStore store)
    {
        _store = store;
    }

    public async Task HandleAsync(UpdateSeasonCommand command, CancellationToken token)
    {
        await using var session = _store.LightweightSession();

        var movie = await session.Events.AggregateStreamAsync<Movie>(command.MovieId);

        if (movie is null)
            throw new Exception("Movie not found.");

        var season = movie.Seasons.FirstOrDefault(x => x.Id == command.SeasonId);

        if (season is null)
            throw new Exception("Season not found.");

        if (season.Name == command.Name)
            return;

        session.Events.Append( command.MovieId,
            new SeasonRenamed(
                command.MovieId,
                command.SeasonId,
                command.Name));

        await session.SaveChangesAsync(token);
    }
}