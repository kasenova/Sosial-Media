using eApp.Core.Domain.Posts;
using eApp.Core.Domain.Posts.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eApp.Pages.Post;

public class OnePostModel : PageModel
{
    private readonly PostRepository _postRepository;
    private readonly CommentRepository _commentRepository;
    public OnePostModel(PostRepository postRepository, CommentRepository commentRepository)
    {
        _postRepository=postRepository;
        _commentRepository=commentRepository;
    }

    public Posts Post {get; set;}

    [BindProperty(SupportsGet = true)]
    public int PostId {get; set;}
    
    public List<Comments> Comments { get; set; }
    [BindProperty]
    public string commentContent{get; set;}

    public async Task<IActionResult> OnGetAsync()
    {
        Post = await _postRepository.GetPostsById(PostId);
        Comments = await _commentRepository.GetComments(PostId);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        string username = HttpContext.Session.GetString("Username"); 
        var Comment = new Comments(PostId, commentContent, username, DateTime.Now);
        await _commentRepository.CreateComment(PostId, Comment);

        return RedirectToPage("/Post/OnePost", new { PostId });
    }

    public async Task<IActionResult> OnPostDeletePostAsync(int postId)
    {
        var commentDelete = await _commentRepository.DeletePostComments(postId);
        if(commentDelete)
        {
            var postDelete = await _postRepository.DeletePost(postId);
            if(postDelete)
            {
                //..
                return RedirectToPage("/Index");
            }
        }
        return Page();
    }
    public async Task<IActionResult> OnPostDeleteCommentAsync(int commentId)
    {
        var commentDelete = await _commentRepository.DeleteComment(commentId);
        if(commentDelete)
        {
            return RedirectToPage("/Post/OnePost", new { PostId });
        }
        else
        {
            return Page();
        }
    }
}