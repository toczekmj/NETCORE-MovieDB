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

    public async Task<Rating?> RetrieveOrDefaultAsync(Guid id)
    {
        var result = await _context.Ratings.SingleOrDefaultAsync(r => r.RatingId == id);
        return result;
    }

    public async Task<Rating> RetrieveMovieRatingOrDefaultAsync(Guid movieId)
    {
        var result = await _context.Ratings.SingleOrDefaultAsync(r => r.MovieId == movieId);
        return result;
    }

    public async Task UpdateAsync(Rating model)
    {
        _context.Update(model);
        await _context.SaveChangesAsync();
    }
}

