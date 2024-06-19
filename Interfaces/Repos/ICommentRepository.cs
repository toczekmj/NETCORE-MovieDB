using Microsoft.EntityFrameworkCore;
using MovieApi.Model;

namespace MovieApi.Interfaces;

public interface ICommentRepository
{
    public Task<IEnumerable<Comment?>?> RetrieveCollectionOrDefault();
    public Task<Comment?> CreateCommentAsync(Comment comment);
    public Task<Comment?> RetrieveOrDefault(Guid id);
    public Task<EntityState?> RemoveCommentAsync(Guid id);
}