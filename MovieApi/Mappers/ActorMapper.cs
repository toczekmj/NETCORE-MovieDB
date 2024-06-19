using MovieApi.Model;
using MovieApi.Model.DTOs;

namespace MovieApi.Mappers;

public static class ActorMapper
{
    public static ActorDto ToActorDto(this Actor actor)
    {
        return new ActorDto(actor.ActorId, actor.firstName, actor.lastName);
    }

    public static ActorDtoWithMovies ToActorDtoWithMovies(this Actor actor)
    {
        return new ActorDtoWithMovies(actor.ActorId, actor.firstName, actor.lastName, actor.Movies);
    }

    public static UpdateActorDto ToUpdateActorDto(this Actor actor)
    {
        return new UpdateActorDto(actor.firstName, actor.lastName);
    }

    public static CreateActorDto ToCreateActorDto(this Actor actor)
    {
        return new CreateActorDto(actor.firstName, actor.lastName);
    }
}