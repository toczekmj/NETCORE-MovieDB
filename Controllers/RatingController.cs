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

    [HttpGet("{movieId:int}")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<Rating?>> GetRating(int? movieId)
    {
        if (!ModelState.IsValid || movieId is null)
            return BadRequest();
        var rating = await _ratingService.GetRatingAsync((int)movieId);
        return rating is null ? NotFound() : Ok(rating);
    }
}