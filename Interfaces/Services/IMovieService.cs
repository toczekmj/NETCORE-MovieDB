using Microsoft.EntityFrameworkCore;
using MovieApi.Model.DTOs;

namespace MovieApi.Interfaces;

public interface IMovieService
{
    Task<MovieDto?> GetMovieByIdAsync(Guid id);
    Task<IEnumerable<MovieDto>?> GetMoviesAsync();
    Task<MovieDto> AddMovieAsync(CreateMovieDto movie);
    Task<MovieDto?> UpdateMovieAsync(Guid id, UpdateMovieDto movie);
    Task<EntityState> DeleteMovieAsync(Guid id);
}