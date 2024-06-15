using MovieApi.Interfaces;
using MovieApi.Mappers;
using MovieApi.Model;
using MovieApi.Model.DTOs;

namespace MovieApi.Service;

public class RatingService : IRatingService
{
    private readonly IRatingRepository _ratingRepository;

    public RatingService(IRatingRepository ratingRepository)
    {
        _ratingRepository = ratingRepository;
    }

    public Rating CreateEmptyRating()
    {
        var rating = new Rating
        {
            MovieId = null,
            Acting = 0,
            Plot = 0,
            Scenography = 0,
            VotesCount = 0,
        };
        return rating;
    }

    

    public async Task<RatingDto?> GetRatingAsync(Guid id)
    {
        var rating = await _ratingRepository.RetrieveOrDefaultAsync(id);
        return rating.ToRatingDto();
    }

    public async Task<RatingDto?> GetMovieRatingAsync(Guid movieId)
    {
        var rating = await _ratingRepository.RetrieveMovieRatingOrDefaultAsync(movieId);
        return rating.ToRatingDto();
    }
    
    public async Task<RatingDto?> Vote(Guid ratingId, UpdateRatingDto ratingDto)
    {
        var currentRating = await _ratingRepository.RetrieveOrDefaultAsync(ratingId);
        
        if (currentRating is null) return null;
        if (ratingDto is
            {
                Acting: < 0 or > 5,
                Plot: < 0 or > 5,
                Scenography: < 0 or > 5,
            }) return null;
        
        currentRating.VotesCount++;
        currentRating.Plot += ratingDto.Plot;
        currentRating.Acting += ratingDto.Acting;
        currentRating.Scenography += ratingDto.Scenography;
        
        await _ratingRepository.UpdateAsync(currentRating);
        
        var output = await _ratingRepository.RetrieveOrDefaultAsync(ratingId);
        return output.ToRatingDto();
    }
}