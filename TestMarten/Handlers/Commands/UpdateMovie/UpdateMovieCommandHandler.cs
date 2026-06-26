using Marten;
using TestMarten.Handlers.Commands.CreateMovie;
using TestMarten.Models;
using TestMarten.Models.Events;

namespace TestMarten.Handlers.Commands.UpdateMovie;

public class UpdateMovieCommandHandler
{

    private readonly IDocumentStore _store;

    public UpdateMovieCommandHandler(IDocumentStore store)
    {
        _store = store;
    }

    public async Task  HandleAsync(UpdateMovieCommand command, CancellationToken ct)
    {

        var session = _store.LightweightSession();

        var movie = await session.Events.AggregateStreamAsync<Movie>(command.Id);

        if (movie == null)
        {
            throw new Exception("Invalid Movie Id");
        }


        var events = new List<object>();

        if (movie.Name != command.Name)
            events.Add(new MovieRenamed(movie.Id,command.Name));

        if (movie.Description != command.Description)
            events.Add(new MovieDescriptionChanged(movie.Id, command.Description));

        if (movie.PublishedAt != command.PublishedAt)
            events.Add(new MoviePublishedAtChanged(movie.Id, command.PublishedAt));

        if (movie.Type != command.Type)
            events.Add(new MovieTypeChanged(movie.Id, command.Type));

        if (events.Count == 0)
            return;




        session.Events.Append(command.Id, events);

        await session.SaveChangesAsync(ct);

        return;


    }
}
