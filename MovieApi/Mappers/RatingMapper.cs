using MovieApi.Model;
using MovieApi.Model.DTOs;

namespace MovieApi.Mappers;

public static class RatingMapper
{
    public static RatingDto ToRatingDto(this Rating? rating)
    {
        if (rating == null) return null;
        return new RatingDto(
            rating.RatingId,
            rating.MovieId,
            rating.Plot,
            rating.Acting,
            rating.Scenography,
            rating.VotesCount
            );
    }
}