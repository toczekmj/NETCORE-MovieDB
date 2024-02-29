using Microsoft.EntityFrameworkCore;
using MovieApi.Data;
using MovieApi.Interfaces;
using MovieApi.Model;

namespace MovieApi.Repository;

public class RatingRepository : IRatingRepository
{
    private readonly AppDbContext _context;

    public RatingRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<Rating> Create(Rating model)
    {
        var result = _context.Ratings.Add(model);
        await _context.SaveChangesAsync();
        return result.Entity;
    }
    
    public async Task<Rating?> RetrieveOrDefault(Rating model)
    {
        var result = await _context.Ratings.SingleOrDefaultAsync(r => r == model);
        return result;
    }

    public async Task<Rating?> RetrieveOrDefault(Guid id)
    {
        var result = await _context.Ratings.SingleOrDefaultAsync(r => r.RatingId == id);
        return result;
    }

    public async Task<Rating> RetrieveMovieRatingOrDefault(Guid movieId)
    {
        var result = await _context.Ratings.SingleOrDefaultAsync(r => r.MovieId == movieId);
        return result;
    }

    public async Task Update(Rating model)
    {
        _context.Update(model);
        await _context.SaveChangesAsync();
    }
}

