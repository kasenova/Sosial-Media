using System.Reflection;
using eApp.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using eApp.Controllers;
using eApp.Core.Domain.Posts.Repository;
using eApp.Core.Domain.Search;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<PostRepository>();
builder.Services.AddScoped<CommentRepository>();
builder.Services.AddScoped<SearchService>();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(3600); // We're keeping this low to facilitate testing. Would normally be higher. Default is 20 minutes
    options.Cookie.IsEssential = true;              // Otherwise we need cookie approval
});

builder.Services.AddDbContext<DataBase>(options =>
{
    options.UseSqlite($"Data Source={Path.Combine("Infrastructure", "Data", "DataBase.db")}");
});

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
        name: "SignOut",
        pattern: "User/SignOut",
        defaults: new { controller = "User", action = "SignOut" });


app.MapRazorPages();

app.Run();
