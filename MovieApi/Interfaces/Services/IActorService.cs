using Microsoft.EntityFrameworkCore;
using MovieApi.Model;
using MovieApi.Model.DTOs;

namespace MovieApi.Interfaces;

public interface IActorService
{
    Task<ActorDto?> GetActorDtoByIdAsync(Guid id);
    Task<Actor?> GetActorByIdAsync(Guid id);
    Task<IEnumerable<ActorDto>?> GetActorsAsync();
    Task<ActorDto?> SaveActorAsync(CreateActorDto actor);
    Task<EntityState> DeleteActorAsync(Guid id);
    Task<ActorDto?> UpdateActorAsync(Guid id, UpdateActorDto actor);
}