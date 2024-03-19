using System.ComponentModel.DataAnnotations;

namespace MovieApi.Model;

public class Comment
{
    [Key] public int CommentId { get; set; }
    public int MovieId { get; set; }
    public string Text { get; set; }
}