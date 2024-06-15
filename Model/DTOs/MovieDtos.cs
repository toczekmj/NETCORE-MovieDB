using System.ComponentModel.DataAnnotations;

namespace MovieApi.Model.DTOs;

public record MovieDto(
    [Required] Guid MovieId,
    [Required] string Title,
    [Required] string Director,
    [Required] string Description,
    [Required] string Genre,
    [Required] [Range(1000, 2024)] int ProductionYear,
    Rating? Rating,
    ICollection<Actor?> Actors,
    ICollection<Comment?>? Comments,
    string PhotoUrl
    );

public record CreateMovieDto(
    [Required] string Title,
    [Required] string Director,
    [Required] string Description,
    [Required] string Genre,
    [Required] [Range(1000, 2024)] int ProductionYear,
    ICollection<Actor?> Actors,
    ICollection<Comment?>? Comments,
    string PhotoUrl
);

public record UpdateMovieDto(
    [Required] string Title,
    [Required] string Director,
    [Required] string Description,
    [Required] string Genre,
    [Required] [Range(1000, 2024)] int ProductionYear,
    ICollection<Actor?> Actors,
    string PhotoUrl
);
    