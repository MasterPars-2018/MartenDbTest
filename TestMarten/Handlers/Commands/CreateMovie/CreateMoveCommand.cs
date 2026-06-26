using TestMarten.Models;

namespace TestMarten.Handlers.Commands.CreateMovie;

public class CreateMoveCommand
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int PublishedAt { get; set; } 
    public MovieType Type { get; set; }
}
