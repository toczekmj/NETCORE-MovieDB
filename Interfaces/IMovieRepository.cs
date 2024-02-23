using MovieApi.Model;

namespace MovieApi.Interfaces;

public interface IMovieRepository
{
    Task<ICollection<Movie>> GetMoviesAsync();
    Task<Movie> CreateNewMovieAsync(Movie movie);
    Task<Movie?> GetMovieByIdAsync(int id);
    void SaveChanges();
}