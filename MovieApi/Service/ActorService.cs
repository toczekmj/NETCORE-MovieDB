using Microsoft.EntityFrameworkCore;
using MovieApi.Interfaces;
using MovieApi.Mappers;
using MovieApi.Model;
using MovieApi.Model.DTOs;

namespace MovieApi.Service;

public class ActorService : IActorService
{
    private readonly IActorRepository _actorRepository;

    public ActorService(IActorRepository actorRepository)
    {
        _actorRepository = actorRepository;
    }
    
    public async Task<ActorDto?> GetActorDtoByIdAsync(Guid id)
    {
        var actor = await _actorRepository.RetrieveOrDefaultAsync(id);
        return actor?.ToActorDto();
    }
    
    public async Task<Actor?> GetActorByIdAsync(Guid id)
    {
        var actor = await _actorRepository.RetrieveOrDefaultAsync(id);
        return actor;
    }

    public async Task<IEnumerable<ActorDto>?> GetActorsAsync()
    {
        var actors = await _actorRepository.RetrieveCollectionOrDefaultAsync();
        return actors.Select(a => a.ToActorDto());
    }

    public async Task<ActorDto?> SaveActorAsync(CreateActorDto actorDto)
    {
        var actor = new Actor()
        {
            firstName = actorDto.firstName,
            lastName = actorDto.lastName,
        };

        var created = await _actorRepository.CreateAsync(actor);
        return created.ToActorDto();
    }
    
    public async Task<EntityState> DeleteActorAsync(Guid id)
    {
        var result = await _actorRepository.Delete(id);
        return result;
    }

    public async Task<ActorDto?> UpdateActorAsync(Guid id, UpdateActorDto actor)
    {
        var result = await _actorRepository.RetrieveOrDefaultAsync(id);
        if (result is null) return null;
        
        result.firstName = actor.firstName;
        result.lastName = actor.lastName;
        
        await _actorRepository.Update(result);
        
        var output = await _actorRepository.RetrieveOrDefaultAsync(id);
        return output.ToActorDto();
    }
}