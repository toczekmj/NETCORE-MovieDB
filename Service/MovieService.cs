using Microsoft.EntityFrameworkCore;
using MovieApi.Interfaces;
using MovieApi.Mappers;
using MovieApi.Model;
using MovieApi.Model.DTOs;

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
    
    public async Task<MovieDto?> GetMovieByIdAsync(Guid id)
    {
        var movie = await _movieRepository.RetrieveOrDefaultAsync(id);
        return movie?.ToMovieDto();
    }

    public async Task<IEnumerable<MovieDto>?> GetMoviesAsync()
    {
        var movies = await _movieRepository.RetrieveCollectionOrDefaultAsync();
        return movies.Select(m => m.ToMovieDto());
    }

    public async Task<MovieDto> AddMovieAsync(CreateMovieDto movieDto)
    {
        var movie = new Movie()
        {
            Title = movieDto.Title,
            Description = movieDto.Description,
            Director = movieDto.Director,
            Genre = movieDto.Genre,
            ProductionYear = movieDto.ProductionYear,
            Actors = movieDto.Actors,
            PhotoUrl = movieDto.PhotoUrl
        };
        
        movie.Rating ??= _ratingService.CreateEmptyRating();
        
        var created = await _movieRepository.CreateAsync(movie);
        return created.ToMovieDto();
    }
    
    public async Task<MovieDto?> UpdateMovieAsync(Guid id, UpdateMovieDto movie)
    {
        var currentMovie = await _movieRepository.RetrieveOrDefaultAsync(id);
        
        if (currentMovie is null)
            return null;

        currentMovie.Actors = new List<Actor?>();
        await _movieRepository.UpdateAsync();

        if(movie.Actors.Any())
        {
            foreach (var actorModel in movie.Actors)
            {
                var actor = await _actorService.GetActorByIdAsync(actorModel!.ActorId);
                currentMovie.Actors.Add(actor);
                await _movieRepository.UpdateAsync();
            }
        }
        
        currentMovie.Title = movie.Title;
        currentMovie.Director = movie.Director;
        currentMovie.Genre = movie.Genre;
        currentMovie.Description = movie.Description;
        currentMovie.ProductionYear = movie.ProductionYear;
        currentMovie.PhotoUrl = movie.PhotoUrl;
        
        await _movieRepository.UpdateAsync();
        var updated = await _movieRepository.RetrieveOrDefaultAsync(id);
        return updated?.ToMovieDto();
    }

    public async Task<EntityState> DeleteMovieAsync(Guid id)
    {
        return await _movieRepository.DeleteAsync(id);
    }
}