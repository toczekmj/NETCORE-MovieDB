using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MovieApi.Data;
using MovieApi.Interfaces;
using MovieApi.Model;

namespace MovieApi.Repository;

public class ActorRepository : IActorRepository
{
    private readonly AppDbContext _context;
    public ActorRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Actor> CreateAsync(Actor model)
    {
        var result = await _context.Actors.AddAsync(model);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<ICollection<Actor>?> RetrieveCollectionOrDefaultAsync()
    {
        var actors = _context.Actors;
        if (actors.IsNullOrEmpty()) return null;
        return await actors.ToListAsync();
    }

    public async Task<Actor?> RetrieveOrDefaultAsync(Guid id)
    {
        return await _context.Actors.SingleOrDefaultAsync(a => a.ActorId == id);
    }

    public async Task Update(Actor model)
    {
        _context.Update(model);
        await _context.SaveChangesAsync();
    }
    
    public async Task<EntityState> Delete(Guid id)
    {
        var actor = await RetrieveOrDefaultAsync(id);

        if (actor is null)
            return EntityState.Unchanged;

        var result = _context.Actors.Remove(actor);
        await _context.SaveChangesAsync();
        return result.State;
    }
}