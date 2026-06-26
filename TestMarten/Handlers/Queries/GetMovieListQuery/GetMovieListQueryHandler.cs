using Marten;
using TestMarten.Models.ProjectionModels;

namespace TestMarten.Handlers.Queries.GetMovieListQuery;

public class GetMovieListQueryHandler
{
    private readonly IDocumentStore _store;

    public GetMovieListQueryHandler(IDocumentStore store)
    {
        _store = store;
    }

    public async Task<IReadOnlyList<MovieListItem>> HandleAsync(GetMovieListQuery query, CancellationToken ct)
    {

        int skip = ((query.PageNumber - 1) > 0 ? 0 : (query.PageNumber - 1)) * query.PageSize; 

        var session = _store.LightweightSession(); 

        var movies = await session.Query<MovieListItem>().Skip(skip).Take(query.PageSize).ToListAsync(ct);

        return movies;

    }

}
