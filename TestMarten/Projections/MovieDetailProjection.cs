using Marten.Events.Aggregation;
using TestMarten.Models.Events;
using TestMarten.Models.ProjectionModels;

namespace TestMarten.Projections;

public partial class MovieDetailProjection : SingleStreamProjection<MovieDetailView,Guid>
{
    public MovieDetailView Create(MovieCreated e)
    {
        return new MovieDetailView
        {
            Id = e.MovieId,
            Name = e.Name,
            Description = e.Description,
            PublishedAt = e.PublishedAt,
            Type = e.Type
        };
    }

    public void Apply(MovieRenamed e, MovieDetailView view)
    {
        view.Name = e.Name;
    }

    public void Apply(MovieDescriptionChanged e, MovieDetailView view)
    {
        view.Description = e.Description;
    }

    public void Apply(SeasonAdded e, MovieDetailView view)
    {
        view.Seasons.Add(new SeasonDto
        {
            Id = e.SeasonId,
            Name = e.Name,
            PartCount = 0
        });
    }

    public void Apply(SeasonRenamed e, MovieDetailView view)
    {
        var season = view.Seasons.FirstOrDefault(x => x.Id == e.SeasonId);

        if (season is null)
            return;

        season.Name = e.Name;
    }

    public void Apply(PartAdded e, MovieDetailView view)
    {
        var season = view.Seasons.FirstOrDefault(x => x.Id == e.SeasonId);

        if (season is null)
            return;

        season.Parts.Add(new PartDto
        {
            Id = e.PartId,
            Title = e.Title,
            Duration = e.Duration
        });

        season.PartCount++;
    }
}