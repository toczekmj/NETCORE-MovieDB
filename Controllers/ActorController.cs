using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Interfaces;
using MovieApi.Model.DTOs;

namespace MovieApi.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class ActorController : ControllerBase
{
    private readonly IActorService _actorService;

    public ActorController(IActorService actorService)
    {
        _actorService = actorService;
    }

    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<ActorDto>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<IEnumerable<ActorDto>?>> GetAllActorsAsync()
    {
        if (!ModelState.IsValid)
            return BadRequest();
        var actors = await _actorService.GetActorsAsync();
        return Ok(actors);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ActorDto))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<ActorDto>> GetActor(Guid id)
    {
        if (!ModelState.IsValid) 
            return BadRequest();
        var result = await _actorService.GetActorDtoByIdAsync(id);
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created, Type = typeof(ActorDto))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<ActorDto>> CreateActorAsync([FromBody] CreateActorDto? actor)
    {
        if (!ModelState.IsValid || actor is null) 
            return BadRequest();
        var newActor = await _actorService.SaveActorAsync(actor);
        return CreatedAtAction(nameof(GetActor), new { id = newActor.ActorId }, newActor);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<EntityState>> DeleteActorAsync(Guid id)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        var result = await _actorService.DeleteActorAsync(id);
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ActorDto))]
    public async Task<ActionResult<ActorDto>> UpdateActorAsync(Guid id, [FromBody] UpdateActorDto actor)
    {
        if (!ModelState.IsValid || actor is null)
            return BadRequest();
        
        var result = await _actorService.UpdateActorAsync(id, actor);
        
        if(result is null) return NotFound();
        return Ok(result);
    }
    
}