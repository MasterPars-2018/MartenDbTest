namespace TestMarten.Handlers.Commands.AddPart;

public record AddPartResponse(
    Guid MovieId,
    Guid SeasonId,
    Guid PartId);