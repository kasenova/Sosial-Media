
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MediatR;
using eApp.Core.Domain.User.Pipelines;

namespace eApp.Pages.User;

public class LoginModel : PageModel
{
    [BindProperty]
    public string Username{get; set;} = default!;
    [BindProperty]
    public string Password {get; set;} = default!;
    public string Error {get; set;} = default!;
    
    private readonly IMediator _mediator;
    public LoginModel(IMediator mediator)
    {
        _mediator=mediator;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var Response = await _mediator.Send(new Login.Request(Username, Password));
        if (Response.Success==true)
        {
            HttpContext.Session.SetString("Username", Username);
            return RedirectToAction("Index");
        }
        Error = Response.Error;
        return Page();
    }
}