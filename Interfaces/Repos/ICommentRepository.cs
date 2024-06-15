using MovieApi.Model;

namespace MovieApi.Interfaces;

public interface ICommentRepository
{
    public Task<IEnumerable<Comment?>?> RetrieveCollectionOrDefault();
    public Task<Comment?> CreateCommentAsync(Comment comment);
    Task<Comment?> RetrieveOrDefault(Guid id);
}