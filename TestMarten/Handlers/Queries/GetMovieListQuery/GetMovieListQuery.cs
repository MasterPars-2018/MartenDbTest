using Microsoft.CodeAnalysis.Options;

namespace TestMarten.Handlers.Queries.GetMovieListQuery;
 
public class GetMovieListQuery {


    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;


};
