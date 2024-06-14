using MovieApi.Model;

namespace MovieApi.Interfaces;

public interface IRatingService
{
    public Rating CreateEmptyRating();
    public Task<Rating?> Vote(Rating rating);
    public Task<Rating?> GetRatingAsync(Guid id);
    public Task<Rating?> GetMovieRatingAsync(Guid movieId);
}