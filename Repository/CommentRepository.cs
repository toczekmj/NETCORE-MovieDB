using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MovieApi.Data;
using MovieApi.Interfaces;
using MovieApi.Model;

namespace MovieApi.Repository;

public class CommentRepository : ICommentRepository
{
    private readonly AppDbContext _context;

    public CommentRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Comment?>?> RetrieveCollectionOrDefault()
    {
        var comments = _context.Comments;
        if (comments.IsNullOrEmpty()) return null;
        return await comments.ToListAsync();
    }

    public async Task<Comment?> CreateCommentAsync(Comment comment)
    {
        var result = await _context.Comments.AddAsync(comment);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<Comment?> RetrieveOrDefault(Guid id)
    {
        return await _context.Comments.SingleOrDefaultAsync(c => c.CommentId == id);
    }
}