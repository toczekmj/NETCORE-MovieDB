using System.Net;
using Microsoft.AspNetCore.Mvc;
using MovieApi.Interfaces;
using MovieApi.Model.DTOs;

namespace MovieApi.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class RatingController : Controller
{
    private readonly IRatingService _ratingService;
    private readonly IMovieService _movieService;

    public RatingController(IRatingService ratingService, IMovieService movieService)
    {
        _ratingService = ratingService;
        _movieService = movieService;
    }

    [HttpPut("{movieId:guid}")]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType((int)HttpStatusCode.NotModified)]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(UpdateRatingDto))]
    public async Task<ActionResult<RatingDto>> VoteByMovieId(Guid movieId, [FromBody] UpdateRatingDto? ratingDto)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        var movie = await _movieService.GetMovieByIdAsync(movieId);
        if (movie is null) return StatusCode((int)HttpStatusCode.InternalServerError); 
        var updatedRating = await _ratingService.Vote(movie.Rating.RatingId, ratingDto);
            return updatedRating is null ? StatusCode((int)HttpStatusCode.InternalServerError) : Ok(updatedRating);
    }
    
    [HttpGet("{ratingId:guid}")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(RatingDto))]
    public async Task<ActionResult<RatingDto?>> GetRating(Guid? ratingId)
    {
        if (!ModelState.IsValid || ratingId is null)
            return BadRequest();
        var rating = await _ratingService.GetRatingAsync((Guid)ratingId);
        return rating is null ? NotFound() : Ok(rating);
    }
    
    [HttpGet("/api/v1/[controller]/byMovieId/{movieId:guid}")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(RatingDto))]
    public async Task<ActionResult<RatingDto?>> GetMovieRating(Guid? movieId)
    {
        if (!ModelState.IsValid || movieId is null)
            return BadRequest();
        var rating = await _ratingService.GetMovieRatingAsync((Guid)movieId);
        return rating is null ? NotFound() : Ok(rating);
    }
}