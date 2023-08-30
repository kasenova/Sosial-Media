using eApp.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using eApp.Core.Domain.User;
using eApp.Core.Domain.User.Pipelines;
using Microsoft.AspNetCore.Mvc;
using eApp.Core.Domain.Posts.Repository;
using eApp.Core.Domain.Posts;

namespace eApp.Pages.User.Profile;

public class MyProfileModel : PageModel
{
    private readonly PostRepository _postRepository;
    private readonly IMediator _mediator;
    public MyProfileModel(IMediator mediator, PostRepository postRepository)
    {
        _postRepository = postRepository;
        _mediator = mediator;
    }

    [BindProperty]
    public string Name { get; set; } = default!;
    [BindProperty]
    public string Apartment { get; set; } = default!;
    [BindProperty]
    public int Zipcode { get; set; } = default!;

    public string? username; 

    public Users user{get; set;}

    public List<Posts> AllPosts {get; set;} 

    public async Task<IActionResult> OnGetAsync()
    {
        username = HttpContext.Session.GetString("Username");
        if (username != null) 
        {
            user = await _mediator.Send(new GetUser.Request(username));
        }
        else
        {
            return RedirectToPage("/User/Login");
        }

        AllPosts = await _postRepository.GetPostsByUsername(username);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    { 
        var existingUser = await _mediator.Send(new GetUser.Request(HttpContext.Session.GetString("Username")));

        if (existingUser == null)
        {
            return RedirectToPage("/User/Login");
        }

        var update = await _mediator.Send(new Update.Request(existingUser.Username, Name, Apartment, Zipcode));
        return RedirectToPage("/User/Profile/MyProfile", new { });
    }
}