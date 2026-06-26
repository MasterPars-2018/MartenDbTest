using Marten;
using TestMarten.Models.ProjectionModels;

namespace TestMarten.Handlers.Queries.GetMovieById;

public class GetMovieByIdQueryHandler
{
    private readonly IDocumentStore _store;

    public GetMovieByIdQueryHandler(IDocumentStore store)
    {
        _store = store;
    }

    public async Task<MovieDetailView> HandleAsync(GetMovieByIdQuery query, CancellationToken token)
    {
      
            var session = _store.LightweightSession();

            var movie = await session.Query<MovieDetailView>().SingleOrDefaultAsync(m => m.Id == query.Id);

            return movie;
 
    } 

}
