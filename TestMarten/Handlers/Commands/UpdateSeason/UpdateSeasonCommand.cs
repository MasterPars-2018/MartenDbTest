namespace TestMarten.Handlers.Commands.UpdateSeason;

public class UpdateSeasonCommand
{
    public Guid MovieId { get; set; }
    public Guid SeasonId { get; set; }
    public string Name { get; set; }
}
