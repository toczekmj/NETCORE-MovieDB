using System.ComponentModel.DataAnnotations;

namespace MovieApi.Model;

public class Rating
{
    [Key]
    public int RatingId { get; set; }
    public int? MovieId { get; set; }
    public int Plot { get; set; } = 0;
    public int Acting { get; set; } = 0;
    public int Scenography { get; set; } = 0;
    public int VotesCount { get; set; } = 0;
}