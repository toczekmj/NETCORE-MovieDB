using MovieApi.Model;

namespace MovieApi.Interfaces;

public interface IActorRepository
{
    Task<ICollection<Actor>> GetActorsAsync();
    Task<Actor> AddActorAsync(Actor actor);
    Task<Actor?> GetActorByIdAsync(int id);
    Task<Actor> UpdateActorAsync(Actor actor);
}