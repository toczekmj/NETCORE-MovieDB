using System.Net;
using Microsoft.AspNetCore.Mvc;
using MovieApi.Interfaces;
using MovieApi.Model;

namespace MovieApi.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class RatingController : Controller
{
    private readonly IRatingService _ratingService;

    public RatingController(IRatingService ratingService)
    {
        _ratingService = ratingService;
    }

    [HttpPut]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType((int)HttpStatusCode.NotModified)]
    public async Task<IActionResult> RateMovie([FromBody] Rating? rating)
    {
        if (!ModelState.IsValid || rating is null)
            return BadRequest();
        var updatedRating = await _ratingService.Vote(rating);
        return updatedRating is null ? StatusCode((int)HttpStatusCode.InternalServerError) : Ok(updatedRating);
    }
    
    [HttpGet("{ratingId:guid}")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<Rating?>> GetRating(Guid? ratingId)
    {
        if (!ModelState.IsValid || ratingId is null)
            return BadRequest();
        var rating = await _ratingService.GetRatingAsync((Guid)ratingId);
        return rating is null ? NotFound() : Ok(rating);
    }
    
    [HttpGet("/api/v1/[controller]/byMovieId/{movieId:guid}")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<Rating?>> GetMovieRating(Guid? movieId)
    {
        if (!ModelState.IsValid || movieId is null)
            return BadRequest();
        var rating = await _ratingService.GetMovieRatingAsync((Guid)movieId);
        return rating is null ? NotFound() : Ok(rating);
    }
}