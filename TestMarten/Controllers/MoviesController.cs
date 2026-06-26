using Microsoft.AspNetCore.Mvc;
using TestMarten.Handlers.Commands.AddPart;
using TestMarten.Handlers.Commands.AddSeason;
using TestMarten.Handlers.Commands.CreateMovie;
using TestMarten.Handlers.Commands.UpdateMovie;
using TestMarten.Handlers.Commands.UpdateSeason;
using TestMarten.Handlers.Queries.GetMovieById;
using TestMarten.Handlers.Queries.GetMovieListQuery;
using TestMarten.Models.ProjectionModels;
using Wolverine;

namespace TestMarten.Controllers
{
    [Route("movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {

        readonly IMessageBus _messageBus;

        public MoviesController(IMessageBus messageBus)
        {
            _messageBus = messageBus;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageSize = 1, [FromQuery] int pageNumber = 10, CancellationToken token = default)
        {

            var movieQuery = new GetMovieListQuery { PageSize = pageSize, PageNumber = pageNumber };
            var movies = await _messageBus.InvokeAsync<IReadOnlyList<MovieListItem>>(movieQuery);

            return Ok(movies);

        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken token)
        {

            var movie = await _messageBus.InvokeAsync<MovieDetailView>(new GetMovieByIdQuery(id));

            return movie == null ? NotFound() : Ok(movie);

        }



        [HttpPost]
        public async Task<IActionResult> CreateMovie([FromBody] CreateMoveCommand command, CancellationToken token)
        {

            var id = await _messageBus.InvokeAsync<Guid>(command, token);

            return Created($"/movies/{id}", new { id }); 
        }




        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateMovie([FromRoute] Guid id,[FromBody] UpdateMovieCommand command, CancellationToken token)
        {
            command.Id = id;
             await _messageBus.InvokeAsync<Guid>(command, token);

            return NoContent();
        }


        [HttpPost]
        [Route("{movieId:guid}/season")]
        public async Task<IActionResult> AddSeason([FromRoute] Guid movieId, [FromBody] AddSeasonCommand command, CancellationToken token)
        {

            if (command.MovieId != movieId)
            {
                return BadRequest("Invalid Movie Id");
            }

            var response = await _messageBus.InvokeAsync<AddSeasonResponse>(command, token);

            return Created($"{response.SeasonId}", response);
        }



        [HttpPut("{movieId:guid}/season/{seasonId:guid}")]
        public async Task<IActionResult> UpdateSeason(
                  [FromRoute] Guid movieId,
                  [FromRoute] Guid seasonId,
                  [FromBody] UpdateSeasonCommand command,
                   CancellationToken token)
        {
            command.MovieId = movieId;
            command.SeasonId = seasonId;

            await _messageBus.InvokeAsync(command, token);

            return NoContent();
        }


        [HttpPost("{movieId:guid}/season/{seasonId:guid}/part")]
        public async Task<IActionResult> AddPart(
                       [FromRoute] Guid movieId,
                       [FromRoute] Guid seasonId,
                       [FromBody] AddPartCommand command,
                       CancellationToken token)
        {
            command.MovieId = movieId;
            command.SeasonId = seasonId;

            var response = await _messageBus.InvokeAsync<AddPartResponse>(command, token);
            return Created($"{response.PartId}", response);
        }


    }



}


