using MovieApi.Model;

namespace MovieApi.Interfaces;

public interface IRatingRepository
{   
    public Task<Rating?> RetrieveOrDefaultAsync(Guid id);
    public Task<Rating?> RetrieveMovieRatingOrDefaultAsync(Guid id);
    public Task UpdateAsync(Rating model);
}