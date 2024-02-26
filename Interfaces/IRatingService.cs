using MovieApi.Model;

namespace MovieApi.Interfaces;

public interface IRatingService
{
    public Rating CreateNewRating();
    public Task UpdateRatingAsync(Rating rating);
}