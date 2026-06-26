using Marten;
using TestMarten.Models;
using TestMarten.Models.Events;

namespace TestMarten.Handlers.Commands.CreateMovie;

public class CreateMovieCommandHandler
{
    public readonly IDocumentStore _store;

    public CreateMovieCommandHandler(IDocumentStore store)
    {
        _store = store;
    }

    public async Task<Guid> HandleAsync(CreateMoveCommand command, CancellationToken ct)
    {

        try
        {

            using var session = _store.LightweightSession();

            var id = Guid.CreateVersion7();

            var events = new object[]
                {
                    new MovieCreated(
                        id ,
                        command.Name,
                        command.Description,
                        command.PublishedAt,
                        command.Type)
                };


            session.Events.StartStream<Movie>(id, events);

            await session.SaveChangesAsync();

            return id;
        }
        catch (Exception)
        {

            throw;
        } 

    }

}