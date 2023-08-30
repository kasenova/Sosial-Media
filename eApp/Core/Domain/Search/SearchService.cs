using eApp.Core.Domain.Posts;
using eApp.Core.Domain.Posts.Repository;
using MediatR;
using eApp.Core.Domain.User.Pipelines;

namespace eApp.Core.Domain.Search;

public class SearchService
{
    private readonly PostRepository _postRepository;
    private readonly IMediator _mediator;
    public SearchService (PostRepository postRepository, IMediator mediator)
    {
        _postRepository = postRepository;
        _mediator = mediator;
    }
    public async Task<List<SearchResult>> Search(string searchString)
    {
        var postResult = await SearchPosts(searchString);
        var userResult = await SearchUser(searchString);

        var searchResults = new List<SearchResult>();
        searchResults.AddRange(postResult);
        searchResults.AddRange(userResult);

        return searchResults;
    }

    public async Task<List<SearchResult>> SearchPosts(string searchString)
    {
        var posts = await _postRepository.GetAllPosts();

        if(string.IsNullOrEmpty(searchString))
        {
            return new List<SearchResult>();
        }

        var postResults = posts
            .Where(p => p.Title.IndexOf(searchString, StringComparison.OrdinalIgnoreCase)>=0 
            || p.Content.IndexOf(searchString, StringComparison.OrdinalIgnoreCase)>=0)
            .Select(p => new SearchResult {Id = p.Id, Type = "Post", Title = p.Title })
            .ToList();
        
        return postResults;
    }

    public async Task<List<SearchResult>> SearchUser(string searchString)
    {
        var users = await _mediator.Send(new GetAllUsers.Request());
        
        if (string.IsNullOrEmpty(searchString))
        {
            return new List<SearchResult>();
        }

        int userId = 0;
        var userResults = users
            .Where(p => p.Username.IndexOf(searchString, StringComparison.OrdinalIgnoreCase)>=0 
            || p.Name.IndexOf(searchString, StringComparison.OrdinalIgnoreCase)>=0)
            .Select(p => new SearchResult {Id = Interlocked.Increment(ref userId), Type = "User", Title = p.Username })
            .ToList();
        return userResults;
    }
}
public class SearchResult
{
    public int Id {get; set;}
    public string Type {get; set;}
    public string Title {get; set;}
}