using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Interfaces;
using MovieApi.Model.DTOs;

namespace MovieApi.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class MovieController : Controller
{
    private readonly IMovieService _movieService;

    public MovieController(IMovieService movieService)
    {
        _movieService = movieService;
    }

    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<MovieDto>))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<IEnumerable<MovieDto?>>> GetAllMoviesAsync()
    {
        if (!ModelState.IsValid)
            return BadRequest();
        var movies = await _movieService.GetMoviesAsync();
        return Ok(movies);
    }

    [HttpGet("{id:guid}")]
    [ActionName("GetMovieByIdAsync")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(MovieDto))]
    public async Task<ActionResult<MovieDto?>> GetMovieByIdAsync(Guid id)
    {
        if (!ModelState.IsValid) 
            return BadRequest();
        var result = await _movieService.GetMovieByIdAsync(id);
        if (result is null)
            return NotFound();
        return Ok(result);
    }
    
    [HttpPut]
    [ProducesResponseType((int)HttpStatusCode.Created, Type = typeof(UpdateMovieDto))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<MovieDto?>> UpdateMovie(Guid id, [FromBody] UpdateMovieDto movie)
    {
        var result = await _movieService.UpdateMovieAsync(id, movie);
        return Ok(result);
    }
    
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created, Type = typeof(CreateMovieDto))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<MovieDto?>> CreateMovieAsync([FromBody] CreateMovieDto? movie)
    {
        if (!ModelState.IsValid || movie is null)
            return BadRequest();
        var newMovie = await _movieService.AddMovieAsync(movie);
        return CreatedAtAction(nameof(GetMovieByIdAsync), new { id = newMovie.MovieId }, newMovie);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(EntityState))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<EntityState>> DeleteMovieAsync(Guid id)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        var result = await _movieService.DeleteMovieAsync(id);
        return Ok(result);
    }
}