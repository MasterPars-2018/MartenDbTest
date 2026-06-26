namespace TestMarten.Models
{
    public class Part
    {
        public Guid Id { get; set; }
        public Guid SeasonId { get; set; }
        public Guid MovieId { get; set; }
        public string Title { get; set; }
        public long Duration { get; set; }
    }
}
