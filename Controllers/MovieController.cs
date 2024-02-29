using System.Net;
using Microsoft.AspNetCore.Mvc;
using MovieApi.Interfaces;
using MovieApi.Model;

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
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<Movie>))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<IEnumerable<Movie>>> GetAllMoviesAsync()
    {
        if (!ModelState.IsValid)
            return BadRequest();
        var movies = await _movieService.GetMoviesAsync();
        return Ok(movies);
    }

    [HttpGet("{id:guid}")]
    [ActionName("GetMovieByIdAsync")]
    public async Task<ActionResult<Movie>> GetMovieByIdAsync(Guid id)
    {
        if (!ModelState.IsValid) 
            return BadRequest();
        var result = await _movieService.GetMovieByIdAsync(id);
        if (result is null)
            return NotFound();
        return Ok(result);
    }
    
    [HttpPut]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<Movie>> UpdateMovie([FromBody] Movie movie)
    {
        var result = await _movieService.UpdateMovieAsync(movie);
        return Ok(result);
    }
    
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<Movie>> CreateMovieAsync([FromBody] Movie? movie)
    {
        if (!ModelState.IsValid || movie is null)
            return BadRequest();
        var newMovie = await _movieService.AddMovieAsync(movie);
        return CreatedAtAction(nameof(GetMovieByIdAsync), new { id = newMovie.MovieId }, newMovie);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<Movie>> DeleteMovieAsync(Guid id)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        var result = await _movieService.DeleteMovieAsync(id);
        return Ok(result);
    }
}