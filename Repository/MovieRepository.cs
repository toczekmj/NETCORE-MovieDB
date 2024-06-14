using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MovieApi.Data;
using MovieApi.Interfaces;
using MovieApi.Model;

namespace MovieApi.Repository;

public class MovieRepository : IMovieRepository
{
    private readonly AppDbContext _context;

    public MovieRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Movie> CreateAsync(Movie model)
    {
        if (!model.Actors.IsNullOrEmpty())
            _context.Actors.AttachRange(model.Actors!);

        var savedMovie = _context.Movies.Add(model);
        await _context.SaveChangesAsync();

        return savedMovie.Entity;
    }

    public async Task<ICollection<Movie>?> RetrieveCollectionOrDefaultAsync()
    {
        return await _context.Movies
            .Include(x => x.Actors)
            .Include(x => x.Rating)
            .ToListAsync();
    }

    public async Task<Movie?> RetrieveOrDefaultAsync(Guid id)
    {
        return await _context.Movies
            .Include(x => x.Actors)
            .Include(x => x.Rating)
            .FirstOrDefaultAsync(m => m.MovieId == id);
    }

    public async Task UpdateAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<EntityState> DeleteAsync(Guid id)
    {
        var movie = await RetrieveOrDefaultAsync(id);
        if (movie is null)
            return EntityState.Unchanged;


        if (movie.Rating is not null)
            _context.Ratings.Remove(movie.Rating);

        var result = _context.Movies.Remove(movie);
        await _context.SaveChangesAsync();
        return result.State;
    }
}