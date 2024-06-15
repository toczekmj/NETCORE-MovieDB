using System.ComponentModel.DataAnnotations;

namespace MovieApi.Model.DTOs;

public record CommentDto(
    [Required] Guid CommentId,
    [Required] Guid MovieId,
    [Required] 
    [StringLength(2500, MinimumLength = 10)] 
    [BanBadWords()] 
    string CommentText
    );

public record CreateCommentDto(
    [Required]
    Guid MovieId,
    [Required] 
    [StringLength(2500, MinimumLength = 10)] 
    [BanBadWords()] 
    string CommentText
);
    
    