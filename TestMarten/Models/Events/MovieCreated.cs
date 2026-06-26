namespace TestMarten.Models.Events;

public record MovieCreated(
    Guid MovieId,
    string Name,
    string Description,
    int PublishedAt,
    MovieType Type);

public record SeasonAdded(
    Guid MovieId,
    Guid SeasonId,
    string Name);

public record SeasonRenamed(
    Guid MovieId,
    Guid SeasonId,
    string Name);

public record PartAdded(
    Guid MovieId,
    Guid SeasonId,
    Guid PartId,
    string Title,
    long Duration);

public record MovieRenamed(Guid MovieId, string Name);

public record MovieDescriptionChanged(Guid MovieId, string Description);

public record MoviePublishedAtChanged(Guid MovieId, int PublishedAt);

public record MovieTypeChanged(Guid MovieId, MovieType Type);

