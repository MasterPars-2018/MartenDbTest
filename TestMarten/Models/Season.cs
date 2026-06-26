namespace TestMarten.Models
{
    public class Season
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int PartCount { get; set; }
        public Guid MovieId { get; set; }
        public List<Part> Parts { get; set; } = [];

    }
}
