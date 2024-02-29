using Microsoft.EntityFrameworkCore;
using MovieApi.Model;

namespace MovieApi.Interfaces;

public interface IActorRepository
{
    public Task<Actor> Create(Actor model);
    public Task<ICollection<Actor>?> RetrieveCollectionOrDefault();
    
    public Task<Actor?> RetrieveOrDefault(Actor model);
    public Task<Actor?> RetrieveOrDefault(Guid id);
    
    public Task Update(Actor model);
    
    public Task<EntityState> Delete(Guid id);
}