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

    protected Comment() {}
    public Comment(
        string? title,
        string? content,
        string userId
    )
    {
        validateTitle(title);
        validateContent(content);
        SetTitle(title);
        SetContent(content);
        UserId = userId;
    }

     public void SetTitle(string? title)
    {
        validateTitle(title);
        Title = title!;
    }

    public void SetContent(string? content)
    {
        validateContent(content);
        Content = content!;
    }

    private void validateTitle(string? title)
    {
        const string name = nameof(Title);
        validateEmpty(title, name);
        validateLength(title!, name, 5, 10);
    }

    private void validateContent(string? content)
    {
        const string name = nameof(Content);
        validateEmpty(content, name);
        validateLength(content!, name, 5, 10);
    }
}