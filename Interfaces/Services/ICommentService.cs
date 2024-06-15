using MovieApi.Model.DTOs;

namespace MovieApi.Interfaces.Services;

public interface ICommentService
{
    public Task<IEnumerable<CommentDto?>?> GetCommentsAsync();
    public Task<CommentDto?> CreateCommentAsync(CreateCommentDto comment);
    public Task<CommentDto?> GetCommentByIdAsync(Guid id);
}