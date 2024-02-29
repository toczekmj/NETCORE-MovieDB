using Microsoft.EntityFrameworkCore;
using MovieApi.Model;

namespace MovieApi.Interfaces;

public interface IActorService
{
    Task<Actor?> GetActorByIdAsync(Guid id);
    Task<ICollection<Actor>?> GetActorsAsync();
    Task<Actor?> SaveActorAsync(Actor actor);
    Task<EntityState> DeleteActorAsync(Guid id);
}