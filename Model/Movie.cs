using System.ComponentModel.DataAnnotations;

namespace MovieApi.Model;

public class Movie
{
    [Key]
    public int MovieId { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Director { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public string Genre { get; set; }
    [Required]
    [Range(1000, 3000)]
    public int ProductionYear { get; set; }

    public Rating? Rating { get; set; }
    public ICollection<Actor?> Actors { get; set; }

    public string PhotoUrl { get; set; }
}