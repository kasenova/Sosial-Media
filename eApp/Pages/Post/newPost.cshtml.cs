using eApp.Core.Domain.Posts;
using eApp.Core.Domain.Posts.Repository;
using eApp.Core.Domain.User;
using eApp.Core.Domain.User.Pipelines;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eApp.Pages.Post;

public class NewPostModel : PageModel
{
    private readonly PostRepository _postRepository;
    private readonly IMediator _mediator;

    [BindProperty]
    public string Title{get; set;}

    [BindProperty]
    public string Content{get; set;}

    public NewPostModel (PostRepository postRepository, IMediator mediator)
    {
        _postRepository = postRepository;
        _mediator = mediator;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        string? username = HttpContext.Session.GetString("Username");
        if(username == null)
        {
            return RedirectToPage("/User/Login");
        }
        var post = new Posts(Title, Content, username, DateTime.Now);
        
        await _postRepository.CreatePost(post);

        return RedirectToPage("/Index"); 
    }
}