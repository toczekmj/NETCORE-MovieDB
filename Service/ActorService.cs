using Microsoft.EntityFrameworkCore;
using MovieApi.Interfaces;
using MovieApi.Model;

namespace MovieApi.Service;

public class ActorService : IActorService
{
    private readonly IRepository<Actor> _actorRepository;

    public ActorService(IRepository<Actor> actorRepository)
    {
        _actorRepository = actorRepository;
    }
    
    public async Task<Actor?> GetActorByIdAsync(int id)
    {
        return await _actorRepository.RetrieveOrDefault(id);
    }

    public async Task<ICollection<Actor>?> GetActorsAsync()
    {
        return await _actorRepository.RetrieveCollectionOrDefault();
    }

    public async Task<Actor?> SaveActorAsync(Actor actor)
    {
        var existing = await _actorRepository.RetrieveOrDefault(actor);
        if(existing is null)
            return await _actorRepository.Create(actor);
        
        existing.firstName = actor.firstName;
        existing.lastName = actor.lastName;
        existing.Movies = actor.Movies;
        await _actorRepository.Update(existing);

        return await _actorRepository.RetrieveOrDefault(actor);
    }
    
    public async Task<EntityState> DeleteActorAsync(int id)
    {
        var result = await _actorRepository.Delete(id);
        return result;
    }
}