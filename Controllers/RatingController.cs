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
    public async Task<ActionResult<Rating>> RateMovie([FromBody] Rating? rating)
    {
        if (!ModelState.IsValid || rating is null)
            return BadRequest();
        var updatedRating = await _ratingService.Vote(rating);
        return Ok(updatedRating);
    }
}