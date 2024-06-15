using System.ComponentModel.DataAnnotations;

namespace MovieApi.Model;

public class Comment
{
    [Key] 
    public Guid CommentId { get; set; }
    [Required] public Guid MovieId { get; set; }
    [Required]
    [StringLength(2500, MinimumLength = 10)]
    [BanBadWords()]
    public string CommentText { get; set; }
}


