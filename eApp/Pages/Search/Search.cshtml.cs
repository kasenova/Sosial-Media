using eApp.Core.Domain.Search;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eApp.Pages.Search;

public class SearchModel : PageModel
{
    private readonly SearchService _searchService;
    public SearchModel(SearchService searchService)
    {
        _searchService = searchService;
    }

    public string searchString{get; set;}
    public List<SearchResult> SearchResults{get; set;}
    
    public async Task<IActionResult> OnGetAsync()
    {
        searchString = Request.Query["searchString"];
        SearchResults = await _searchService.Search(searchString);

        return Page();
    }
}