using System.ComponentModel.DataAnnotations;

namespace eApp.Core.Domain.Posts;

public class Comments 
{
    public Comments(int postId, string content, string username, DateTime date)
    {
        PostId = postId;
        Content=content;
        Username = username;
        Date = date;
    }
    [Key]
    public int Id{get; set;}
    public int PostId{get; set;}
    public string Content{get; set;}
    public string Username{get; set;}
    public DateTime Date {get; set;}
}