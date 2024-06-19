using System.ComponentModel.DataAnnotations;

namespace MovieApi.Model.DTOs;

public record ActorDto(
    [Required] Guid ActorId, 
    [StringLength(20, MinimumLength = 2, ErrorMessage = "Imie powinno zawierać od 2 do 20 znaków")] string firstName,
    [StringLength(20, MinimumLength = 2, ErrorMessage = "Nazwisko powinno zawierać od 2 do 20 znaków")] string lastName
    );

public record ActorDtoWithMovies(
    [Required] Guid ActorId, 
    [StringLength(20, MinimumLength = 2, ErrorMessage = "Imie powinno zawierać od 2 do 20 znaków")] string firstName,
    [StringLength(20, MinimumLength = 2, ErrorMessage = "Nazwisko powinno zawierać od 2 do 20 znaków")] string lastName,
    ICollection<Movie?>? Movies
);

public record CreateActorDto(
    [StringLength(20, MinimumLength = 2, ErrorMessage = "Imie powinno zawierać od 2 do 20 znaków")] string firstName,
    [StringLength(20, MinimumLength = 2, ErrorMessage = "Nazwisko powinno zawierać od 2 do 20 znaków")] string lastName
    );

public record UpdateActorDto(
    [StringLength(20, MinimumLength = 2, ErrorMessage = "Imie powinno zawierać od 2 do 20 znaków")] string firstName,
    [StringLength(20, MinimumLength = 2, ErrorMessage = "Nazwisko powinno zawierać od 2 do 20 znaków")] string lastName
    );
