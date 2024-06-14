using MovieApi.Model;
using MovieApi.Model.DTOs;

namespace MovieApi.Mappers;

public static class MovieMapper
{
    public static MovieDto ToMovieDto(this Movie movie)
    {
        return new MovieDto(
            movie.MovieId, 
            movie.Title, 
            movie.Director, 
            movie.Description, 
            movie.Genre, 
            movie.ProductionYear, 
            movie.Rating, 
            movie.Actors, 
            movie.PhotoUrl
            );
    }
    
    public static CreateMovieDto ToCreateMovieDto(this Movie movie)
    {
        return new CreateMovieDto(
            movie.Title, 
            movie.Director, 
            movie.Description, 
            movie.Genre, 
            movie.ProductionYear, 
            movie.Rating, 
            movie.Actors, 
            movie.PhotoUrl
        );
    }
}