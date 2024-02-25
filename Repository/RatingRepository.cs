using Microsoft.EntityFrameworkCore;
using MovieApi.Data;
using MovieApi.Interfaces;
using MovieApi.Model;

namespace MovieApi.Repository;

public class RatingRepository : IRepository<Rating>
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

    public async Task<ICollection<Rating>?> RetrieveCollectionOrDefault()
    {
        var result = await _context.Ratings.ToListAsync();
        return result;
    }

    public async Task<Rating?> RetrieveOrDefault(Rating model)
    {
        var result = await _context.Ratings.SingleOrDefaultAsync(r => r == model);
        return result;
    }

    public async Task<Rating?> RetrieveOrDefault(int id)
    {
        var result = await _context.Ratings.SingleOrDefaultAsync(r => r.RatingId == id);
        return result;
    }

    public async Task Update(Rating model)
    {
        _context.Update(model);
        await _context.SaveChangesAsync();
    }
    
    public async Task Update()
    {
        await _context.SaveChangesAsync();
    }

    public Task<EntityState> Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task<EntityState> Delete(Rating model)
    {
        throw new NotImplementedException();
    }
}