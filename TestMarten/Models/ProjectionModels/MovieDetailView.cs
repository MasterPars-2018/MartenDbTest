namespace TestMarten.Models.ProjectionModels;

public class MovieDetailView
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int PublishedAt { get; set; }
    public MovieType Type { get; set; }

    public List<SeasonDto> Seasons { get; set; } = new();
}

public class SeasonDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int PartCount { get; set; } =0;
    public List<PartDto> Parts { get; set; } = new();
}

public class PartDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public long Duration { get; set; }
}