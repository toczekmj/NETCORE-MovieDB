using MovieApi.Interfaces;
using MovieApi.Model;

namespace MovieApi.Service;

public class RatingService : IRatingService
{
    private readonly IRepository<Rating> _ratingRepository;

    public RatingService(IRepository<Rating> ratingRepository)
    {
        _ratingRepository = ratingRepository;
    }
    
    public Rating CreateNewRating()
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

    public async Task<Rating?> GetRatingAsync(int id)
    {
        return await _ratingRepository.RetrieveOrDefault(id); 
    }

    public async Task UpdateRatingAsync(Rating rating)
    {
        var currentRating = await _ratingRepository.RetrieveOrDefault(rating);
        if(currentRating is null) return;
        currentRating.Acting = rating.Acting;
        currentRating.Plot = rating.Plot;
        currentRating.Scenography = rating.Scenography;
        currentRating.VotesCount = rating.VotesCount;
        await _ratingRepository.Update(currentRating);
    }
}