using MovieApi.Model;
using MovieApi.Model.DTOs;

namespace MovieApi.Mappers;

public static class CommentMapper
{
    public static CommentDto ToCommentDto(this Comment comment)
    {
        return new CommentDto(comment.CommentId, comment.MovieId, comment.CommentText);
    }
}