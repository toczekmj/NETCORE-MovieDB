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
}