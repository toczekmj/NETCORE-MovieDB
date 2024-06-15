using MovieApi.Interfaces;
using MovieApi.Interfaces.Services;
using MovieApi.Mappers;
using MovieApi.Model;
using MovieApi.Model.DTOs;

namespace MovieApi.Service;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;

    public CommentService(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }
    
    public async Task<IEnumerable<CommentDto?>?> GetCommentsAsync()
    {
        var comments = await _commentRepository.RetrieveCollectionOrDefault();
        return comments?.Select(c => c?.ToCommentDto());
    }

    public async Task<CommentDto?> CreateCommentAsync(CreateCommentDto commentDto)
    {
        var comment = new Comment
        {
            MovieId = commentDto.MovieId,
            CommentText = commentDto.CommentText,
        };
        var createdComment = await _commentRepository.CreateCommentAsync(comment);
        return createdComment?.ToCommentDto();
    }

    public async Task<CommentDto?> GetCommentByIdAsync(Guid id)
    {
        var comment = await _commentRepository.RetrieveOrDefault(id);
        return comment?.ToCommentDto();
    }
}