using JasperFx.Events.Daemon;
using TestMarten.Models;

namespace TestMarten.Handlers.Commands.UpdateMovie;

public class UpdateMovieCommand
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int PublishedAt { get; set; }
    public MovieType Type { get; set; }
}
