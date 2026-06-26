using Marten.Events.Aggregation;
using TestMarten.Models.Events;
using TestMarten.Models.ProjectionModels;

namespace TestMarten.Projections
{
    public partial class MoveListItemProjection : SingleStreamProjection<MovieListItem, Guid>
    {

        public MovieListItem Create(MovieCreated movie)
        {

            return new MovieListItem
            {
                Id = movie.MovieId,
                Name = movie.Name,
                Description = movie.Description,
                PublishedAt = movie.PublishedAt
            };

        }

        public void Apply(MovieRenamed e, MovieListItem state)
        {
            state.Name = e.Name;
     
        }

        public void Apply(MovieDescriptionChanged e, MovieListItem state)
        {
            state.Description = e.Description;

        }

        public void Apply(MoviePublishedAtChanged e, MovieListItem state)
        {
            state.PublishedAt = e.PublishedAt;

        }

        public void Apply(MovieTypeChanged e, MovieListItem state)
        {
            state.Type = e.Type;

        }


    }
}
