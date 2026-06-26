namespace TestMarten.Handlers.Commands.AddPart;


public class AddPartCommand
{
    public Guid MovieId { get; set; }
    public Guid SeasonId { get; set; }

    public string Title { get; set; } = default!;

    public long Duration { get; set; }
}
