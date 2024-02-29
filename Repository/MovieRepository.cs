using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MovieApi.Data;
using MovieApi.Interfaces;
using MovieApi.Model;

namespace MovieApi.Repository;

public class MovieRepository : IRepository<Movie>
{
    private readonly AppDbContext _context;

    public MovieRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Movie> Create(Movie model)
    {
        if (!model.Actors.IsNullOrEmpty())
            _context.Actors.AttachRange(model.Actors!);

        var savedMovie = _context.Movies.Add(model);
        await _context.SaveChangesAsync();

        return savedMovie.Entity;
    }

    public async Task<ICollection<Movie>?> RetrieveCollectionOrDefault()
    {
        return await _context.Movies
            .Include(x => x.Actors)
            .Include(x => x.Rating)
            .ToListAsync();
    }

    public async Task<Movie?> RetrieveOrDefault(Movie model)
    {
        return await _context.Movies
            .Include(x => x.Actors)
            .Include(x => x.Rating)
            .FirstOrDefaultAsync(m => m.MovieId == model.MovieId);
    }

    public async Task<Movie?> RetrieveOrDefault(int id)
    {
        return await _context.Movies
            .Include(x => x.Actors)
            .Include(x => x.Rating)
            .FirstOrDefaultAsync(m => m.MovieId == id);
    }

    public async Task Update(Movie model)
    {
        _context.Update(model);
        await _context.SaveChangesAsync();
    }

    public async Task Update()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<EntityState> Delete(int id)
    {
        var movie = await RetrieveOrDefault(id);
        if (movie is null)
            return EntityState.Unchanged;


        if (movie.Rating is not null)
            _context.Ratings.Remove(movie.Rating);

        var result = _context.Movies.Remove(movie);
        await _context.SaveChangesAsync();
        return result.State;
    }

    public async Task<EntityState> Delete(Movie model)
    {
        return await Delete(model.MovieId);
    }
}