using Microsoft.EntityFrameworkCore;
using MovieApi.Model;

namespace MovieApi.Interfaces;

public interface IActorRepository
{
    public Task<Actor> CreateAsync(Actor model);
    public Task<ICollection<Actor>?> RetrieveCollectionOrDefaultAsync();
    public Task<Actor?> RetrieveOrDefaultAsync(Guid id);
    public Task Update(Actor model);
    public Task<EntityState> Delete(Guid id);
}