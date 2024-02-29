using Microsoft.EntityFrameworkCore;
using MovieApi.Interfaces;
using MovieApi.Model;

namespace MovieApi.Service;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRepository;
    private readonly IRatingService _ratingService;
    private readonly IActorService _actorService;

    public MovieService(IMovieRepository movieRepository,
        IRatingService ratingService,
        IActorService actorService)
    {
        _movieRepository = movieRepository;
        _ratingService = ratingService;
        _actorService = actorService;
    }
    
    public async Task<Movie?> GetMovieByIdAsync(Guid id)
    {
        return await _movieRepository.RetrieveOrDefault(id);
    }

    public async Task<ICollection<Movie>?> GetMoviesAsync()
    {
        return await _movieRepository.RetrieveCollectionOrDefault();
    }

    public async Task<Movie> AddMovieAsync(Movie movie)
    {
        if (movie.Rating is null)
            movie.Rating = _ratingService.CreateEmptyRating();
        
        return await _movieRepository.Create(movie);
    }

    public async Task<Movie?> UpdateMovieAsync(Movie movie)
    {
        var currentMovie = await _movieRepository.RetrieveOrDefault(movie);
        
        if (currentMovie is null)
            return null;
            
        if (movie.Rating != null) 
            await _ratingService.UpdateRatingAsync(movie.Rating);

        currentMovie.Actors = new List<Actor?>();
        await _movieRepository.Update();

        if(movie.Actors.Any())
        {
            foreach (var actorModel in movie.Actors)
            {
                var actor = await _actorService.GetActorByIdAsync(actorModel!.ActorId);
                currentMovie.Actors.Add(actor);
                await _movieRepository.Update();
            }
        }
        
        currentMovie.Title = movie.Title;
        currentMovie.Director = movie.Director;
        currentMovie.Genre = movie.Genre;
        currentMovie.Description = movie.Description;
        currentMovie.ProductionYear = movie.ProductionYear;
        currentMovie.PhotoUrl = movie.PhotoUrl;
        
        await _movieRepository.Update();
        return await _movieRepository.RetrieveOrDefault(movie);
    }

    public async Task<EntityState> DeleteMovieAsync(Guid id)
    {
        return await _movieRepository.Delete(id);
    }
}