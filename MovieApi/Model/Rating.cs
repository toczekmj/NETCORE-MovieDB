using System.ComponentModel.DataAnnotations;

namespace MovieApi.Model;

public class Rating
{
    [Key]
    public Guid RatingId { get; set; }
    public Guid? MovieId { get; set; }
    public int Plot { get; set; } = 0;
    public int Acting { get; set; } = 0;
    public int Scenography { get; set; } = 0;
    public int VotesCount { get; set; } = 0;
}