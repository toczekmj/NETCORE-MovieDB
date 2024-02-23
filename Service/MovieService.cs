using Microsoft.EntityFrameworkCore;
using MovieApi.Interfaces;
using MovieApi.Model;

namespace MovieApi.Service;

public class MovieService : IMovieService
{
    private readonly IRepository<Movie> _movieRepository;
    private readonly IRatingService _ratingService;

    public MovieService(IRepository<Movie> movieRepository, IRatingService ratingService)
    {
        _movieRepository = movieRepository;
        _ratingService = ratingService;
    }
    
    public async Task<Movie?> GetMovieByIdAsync(int id)
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
            movie.Rating = _ratingService.CreateNewRating();
        
        return await _movieRepository.Create(movie);
    }

    public async Task<Movie?> UpdateMovieAsync(Movie movie)
    {
        var existingMovie = await _movieRepository.RetrieveOrDefault(movie);
        if (existingMovie is null) return null;
        
        //TODO: take a look why it does not work 
        existingMovie.Title = movie.Title;
        existingMovie.Director = movie.Director;
        existingMovie.ProductionYear = movie.ProductionYear;
        existingMovie.Rating = movie.Rating;
        existingMovie.Actors = movie.Actors;
        existingMovie.Genre = movie.Genre;

        await _movieRepository.Update();

        return await _movieRepository.RetrieveOrDefault(movie);
    }

    public async Task<EntityState> DeleteMovieAsync(int id)
    {
        return await _movieRepository.Delete(id);
    }
}