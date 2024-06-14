using Microsoft.EntityFrameworkCore;
using MovieApi.Model;

namespace MovieApi.Interfaces;

public interface IMovieRepository
{
    public Task<Movie> Create(Movie model);
    public Task<ICollection<Movie>?> RetrieveCollectionOrDefault();
    
    public Task<Movie?> RetrieveOrDefault(Movie model);
    public Task<Movie?> RetrieveOrDefault(Guid id);
    public Task Update();
    
    public Task<EntityState> Delete(Guid id);
}