using MovieApi.Model;
using MovieApi.Model.DTOs;

namespace MovieApi.Interfaces;

public interface IRatingService
{
    public Rating CreateEmptyRating();
    public Task<RatingDto?> Vote(Guid ratingId, UpdateRatingDto? ratingDto);
    public Task<RatingDto?> GetRatingAsync(Guid id);
    public Task<RatingDto?> GetMovieRatingAsync(Guid movieId);
}