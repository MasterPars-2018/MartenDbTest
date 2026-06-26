using Marten;
using TestMarten.Models;
using TestMarten.Models.Events;

namespace TestMarten.Handlers.Commands.AddPart;

public class AddPartCommandHandler
{
    private readonly IDocumentStore _store;

    public AddPartCommandHandler(IDocumentStore store)
    {
        _store = store;
    }

    public async Task<AddPartResponse> HandleAsync(AddPartCommand command, CancellationToken ct)
    {
        await using var session = _store.LightweightSession();

        var movie = await session.Events.AggregateStreamAsync<Movie>(
            command.MovieId ,token: ct);

        if (movie is null)
            throw new Exception("Movie not found.");

        var season = movie.Seasons.FirstOrDefault(x => x.Id == command.SeasonId);

        if (season is null)
            throw new Exception("Season not found.");

        var partId = Guid.CreateVersion7();

        session.Events.Append(
            command.MovieId,
            new PartAdded(
                command.MovieId,
                command.SeasonId,
                partId,
                command.Title,
                command.Duration));

        await session.SaveChangesAsync(ct);

        return new AddPartResponse(
            command.MovieId,
            command.SeasonId,
            partId);
    }
}