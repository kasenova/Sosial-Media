using eApp.Core.Domain.Posts;
using eApp.Core.Domain.Posts.Repository;
using eApp.Core.Domain.User;
using eApp.Core.Domain.User.Pipelines;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eApp.Pages.User;

public class OneProfileModel : PageModel
{
    private readonly IMediator _mediator;
    private readonly PostRepository _postRepository;
    public OneProfileModel(IMediator mediator, PostRepository postRepository)
    {
        _mediator = mediator;
        _postRepository=postRepository;
    }

    public Users User {get; set;}
    public Posts Post {get; set;}

    [BindProperty(SupportsGet = true)]
    public string UserName {get; set;}

    [BindProperty(SupportsGet = true)]
    public int PostId {get; set;}

    public async Task<IActionResult> OnGetAsync()
    {
        if(string.IsNullOrEmpty(UserName))
        {
            return NotFound();
        }
        
        User = await _mediator.Send(new GetUser.Request(UserName));
        //Post = await _postRepository.GetPostsById(PostId);
        return Page();
    }
}