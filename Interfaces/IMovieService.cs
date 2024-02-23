using Microsoft.EntityFrameworkCore;
using MovieApi.Model;

namespace MovieApi.Interfaces;

public interface IMovieService
{
    Task<Movie?> GetMovieByIdAsync(int id);
    Task<ICollection<Movie>?> GetMoviesAsync();
    Task<Movie> AddMovieAsync(Movie movie);
    Task<Movie?> UpdateMovieAsync(Movie movie);
    Task<EntityState> DeleteMovieAsync(int id);
}