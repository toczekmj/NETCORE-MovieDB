namespace MovieApi.Model.DTOs;

public record RatingDto(Guid RatingId, Guid? MovieId, int Plot, int Acting, int Scenography, int VotesCount);
public record UpdateRatingDto(int Plot, int Acting, int Scenography);