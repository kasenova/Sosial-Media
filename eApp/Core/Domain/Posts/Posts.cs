using System.ComponentModel.DataAnnotations;
using eApp.Core.Domain.User;

namespace eApp.Core.Domain.Posts;

public class Posts
{
    public Posts(string title, string content, string username, DateTime date)
    {
        Title = title;
        Content = content;
        Username = username;
        Date = date;
        Comments = new List<Comments>();
    }
    [Key]
    public int Id {get; set;}
    public string Title{get; set;}
    public string Content{get; set;} 
    public string Username{get; set;}
    public DateTime Date {get; set;}
    public List<Comments> Comments{get; set;}
}