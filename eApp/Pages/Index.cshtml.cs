using Microsoft.AspNetCore.Mvc.RazorPages;
using eApp.Infrastructure.Data;
using eApp.Core.Domain.Posts.Repository;
using eApp.Core.Domain.Posts;

namespace eApp.Pages;

public class IndexModel : PageModel
{
    private readonly PostRepository _postRepository;
    public IndexModel(PostRepository postRepository)
    {
        _postRepository=postRepository;
    }

    public List<Posts> Posts {get; set;} = new();
    
    public async Task OnGetAsync()
    {
        Posts = await _postRepository.GetAllPosts();
    }
}
