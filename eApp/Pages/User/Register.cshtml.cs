using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using eApp.Infrastructure.Data;
using MediatR;
using eApp.Core.Domain.User.Pipelines;

namespace eApp.Pages.User;

public class RegisterModel : PageModel
{
    private readonly IMediator _mediator;

    [BindProperty]
    public string Username { get; set; } = default!;
    [BindProperty]
    public string Password { get; set; } = default!;
    [BindProperty]
    public string Name { get; set; } = default!;
    [BindProperty]
    public string Apartment { get; set; } = default!;
    [BindProperty]
    public int Zipcode { get; set; } = default!;

    public string Error { get; set; } = default!;
    public bool SucRegister { get; set; } = false;

    public RegisterModel (IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var Response = await _mediator.Send(new Register.Request(Username, Password, Name, Apartment, Zipcode));

        Error = Response.Error;
        return RedirectToPage("/User/Login");
    }
}