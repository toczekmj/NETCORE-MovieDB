using Microsoft.EntityFrameworkCore;
using MovieApi.Model;

namespace MovieApi.Interfaces;

public interface IMovieRepository
{
    public Task<Movie> CreateAsync(Movie model);
    public Task<ICollection<Movie>?> RetrieveCollectionOrDefaultAsync();
    public Task<Movie?> RetrieveOrDefaultAsync(Guid id);
    public Task UpdateAsync();
    public Task<EntityState> DeleteAsync(Guid id);
}