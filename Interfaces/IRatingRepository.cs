using MovieApi.Model;

namespace MovieApi.Interfaces;

public interface IRatingRepository
{
    public Task<Rating> CreateNewRating(int movieid);
}