using TestMarten.Models.Events;

namespace TestMarten.Models
{
    public class Movie
    {
        
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PublishedAt { get; set; }
        public MovieType Type { get; set; }
        public List<Season> Seasons { get; set; } = [];

        public void Apply(MovieCreated e)
        {
            Id = e.MovieId;
            Name = e.Name;
            Description = e.Description;
            PublishedAt = e.PublishedAt;
            Type = e.Type;
        }

    

        public void Apply(MovieRenamed e)
        {
            Name = e.Name;
        }

        public void Apply(MovieDescriptionChanged e)
        {
            Description = e.Description;
        }

        public void Apply(MoviePublishedAtChanged e)
        {
            PublishedAt = e.PublishedAt;
        }


        public void Apply(MovieTypeChanged e)
        {
            Type = e.Type;  
        }

        public void Apply(SeasonAdded e)
        {
            Seasons.Add(new Season
            {
                Id = e.SeasonId,
                Name = e.Name
            });
        }

        public void Apply(SeasonRenamed e)
        {
            var season = Seasons.First(x => x.Id == e.SeasonId);

            season.Name = e.Name;
        }

        public void Apply(PartAdded e)
        {
            var season = Seasons.First(x => x.Id == e.SeasonId);

            season.Parts.Add(new Part
            {
                Id = e.PartId,
                MovieId = e.MovieId,
                SeasonId = e.SeasonId,
                Title = e.Title,
                Duration = e.Duration
            });

            season.PartCount++;
        }
    }
}
