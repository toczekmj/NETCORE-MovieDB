using Microsoft.EntityFrameworkCore;
using MovieApi.Model;

namespace MovieApi.Interfaces;

public interface IRatingRepository
{
    public Task<Rating> Create(Rating model);
    public Task<Rating?> RetrieveOrDefault(Rating model);
    public Task<Rating?> RetrieveOrDefault(Guid id);
    public Task<Rating?> RetrieveMovieRatingOrDefault(Guid id);
    
    public Task Update(Rating model);
}