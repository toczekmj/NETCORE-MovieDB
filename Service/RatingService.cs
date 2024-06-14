using MovieApi.Interfaces;
using MovieApi.Model;

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

    public async Task<Rating?> GetRatingAsync(Guid id)
    {
        return await _ratingRepository.RetrieveOrDefaultAsync(id);
    }

    public async Task<Rating?> GetMovieRatingAsync(Guid movieId)
    {
        return await _ratingRepository.RetrieveMovieRatingOrDefaultAsync(movieId);
    }

    public async Task<Rating?> Vote(Rating rating)
    {
        var currentRating = await _ratingRepository.RetrieveOrDefaultAsync(rating.RatingId);
        if (currentRating is null)
            return null;
        if (rating is
            {
                Acting: > 5 or < 0,
                Scenography: > 5 or < 0,
                Plot: > 5 or < 0
            })
            return null;

        currentRating.VotesCount++;
        currentRating.Scenography += rating.Scenography;
        currentRating.Acting += rating.Acting;
        currentRating.Plot += rating.Plot;
        await _ratingRepository.UpdateAsync(currentRating);

        return await _ratingRepository.RetrieveOrDefaultAsync(rating.RatingId);
    }
}