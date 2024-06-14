using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Interfaces;
using MovieApi.Model;

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
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<Actor>))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetAllActorsAsync()
    {
        if (!ModelState.IsValid)
            return BadRequest();
        var actors = await _actorService.GetActorsAsync();
        return Ok(actors);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Actor>> GetActor(Guid id)
    {
        if (!ModelState.IsValid) 
            return BadRequest();
        var result = await _actorService.GetActorByIdAsync(id);
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<Actor>> CreateActorAsync([FromBody] Actor? actor)
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
    
}