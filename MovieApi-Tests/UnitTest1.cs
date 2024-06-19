using FluentAssertions;
using Moq;
using MovieApi.Controllers;
using MovieApi.Interfaces;
using MovieApi.Mappers;
using MovieApi.Model;
using MovieApi.Model.DTOs;
using MovieApi.Service;

namespace MovieApi_Tests;

public class UnitTest1
{   
    [Fact]
    public async Task Test1()
    {
        var id = new Guid("BA161C4B-E843-4EA9-87CA-87F4C05489A7");
        var actor = new Actor
        {
            ActorId = id,
            firstName = "John",
            lastName = "Doe",
        };
        
        var actorService = new Mock<IActorService>();
        actorService.Setup(x => x.GetActorDtoByIdAsync(It.IsAny<Guid>())).ReturnsAsync(actor.ToActorDto);

        var controller = new ActorController(actorService.Object);
        var getById = await controller.GetActor(id);
        var returnActor = getById.Value;

        Assert.NotNull(getById);
        Assert.Equal(actor.ToActorDto(), returnActor);
    }
}