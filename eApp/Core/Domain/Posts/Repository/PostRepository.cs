using eApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace eApp.Core.Domain.Posts.Repository;

public class PostRepository 
{
    private readonly DataBase _db;

    public PostRepository(DataBase db)
    {
        _db = db;
    }

    public async Task<Posts> CreatePost(Posts post)
    {
        _db.Set<Posts>().Add(post);
        await _db.SaveChangesAsync();
        return post;
    }

    public async Task<Posts> GetPostsById(int postId)
    {
       return await _db.Posts.FirstOrDefaultAsync(p => p.Id == postId);
    }

    public async Task<List<Posts>> GetAllPosts()
    {
        return await _db.Posts.ToListAsync();
    }

    public async Task<List<Posts>> GetPostsByUsername(string postUsername)
    {
        var userPost = await _db.Posts
            .Where(u => u.Username == postUsername)
            .ToListAsync();
       
        return userPost;
    }

    public async Task<bool> DeletePost(int postId)
    {
        var post = await _db.Posts.FirstOrDefaultAsync(p => p.Id == postId);
        if (post != null)
        {
            _db.Remove(post);
            await _db.SaveChangesAsync();
            return true;
        }
        return false;
    }
}