using Marten;
using RTools_NTS.Util;
using TestMarten.Models;
using TestMarten.Models.Events;

namespace TestMarten.Handlers.Commands.AddSeason;

public class AddSeasonCommandHandler
{
    private readonly IDocumentStore _store;

    public AddSeasonCommandHandler(IDocumentStore store)
    {
        _store = store;
    }

    public async Task<AddSeasonResponse> HandleAsync(AddSeasonCommand command,CancellationToken ct)
    { 

        await using var session = _store.LightweightSession();


        var movie = await session.Events.AggregateStreamAsync<Movie>(
            command.MovieId, token: ct);


        if (movie == null)
        {
            throw new Exception("Invalid Movie Id");
        }
         
        var seasonAddedEvent = new SeasonAdded(command.MovieId, Guid.CreateVersion7(), command.Name); 

        session.Events.Append(command.MovieId, seasonAddedEvent);

        await session.SaveChangesAsync();


        return new AddSeasonResponse(command.MovieId, seasonAddedEvent.SeasonId);

    } 



}
