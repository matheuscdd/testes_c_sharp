using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("Comments")]
public class Comment: Entity
{
    public string Title { get; private set; }
    public string Content { get; private set; }
    public DateTime CreatedOn { get; private set; } = DateTime.Now;
    public string UserId { get; private set; }
    public User? User { get; set; }
    // Navigation property serve para ajudar nas queries 
    public Stock? Stock { get; set; }

    // TODO - inserir validações no construtor
    public Comment(
        string title,
        string content,
        string userId
    )
    {
        Title = title;
        Content = content;
        UserId = userId;
    }
}