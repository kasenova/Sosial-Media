using eApp.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eApp.Controllers;

public class UserController : Controller
{
    [HttpPost]
    public IActionResult SignOut()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login", "User");
    }
}