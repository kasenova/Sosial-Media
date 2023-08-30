using eApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace eApp.Core.Domain.Posts.Repository;

public class CommentRepository 
{
    private readonly DataBase _db;

    public CommentRepository(DataBase db)
    {
        _db = db;
    }

    public async Task<Comments> CreateComment(int postId, Comments comment)
    {
        var post = await _db.Posts.FindAsync(postId);
        if (post == null)
        {
            throw new ArgumentException("Invalid Id");
        }
        comment.PostId = postId;
        post.Comments.Add(comment);
        await _db.SaveChangesAsync();

        return comment;
    }

    public async Task<List<Comments>> GetComments(int postId)
    {
        var postComments = await _db.Comments
            .Where(c => c.PostId==postId)
            .ToListAsync();

        return postComments; 
    }
    
    public async Task<bool> DeletePostComments(int postId)
    {
        var postComments = await _db.Comments
            .Where(c => c.PostId==postId)
            .ToListAsync();

        if (postComments !=null && postComments.Any())
        {
            _db.RemoveRange(postComments);
            await _db.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<bool> DeleteComment(int commentId)
    {
        var comment = await _db.Comments.FirstOrDefaultAsync(c => c.Id == commentId);
        if (comment != null)
        {
            _db.Remove(comment);
            await _db.SaveChangesAsync();
            return true;
        }
        return false;
    }
}