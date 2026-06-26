namespace TestMarten.Models.ProjectionModels
{
    public class MovieListItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PublishedAt { get; set; }
        public MovieType Type { get; set; }

    }
}
