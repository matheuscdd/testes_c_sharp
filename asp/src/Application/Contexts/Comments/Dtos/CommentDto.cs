using Domain.Entities;

namespace Application.Contexts.Comments.Dtos;

public class CommentDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedOn { get; set; }
    public string UserId { get; set; }
    public string UserName { get; set; }
    public int StockId { get; set; }

    public CommentDto() {}

    public CommentDto(
        string title,
        string content
    )
    {
        Title = title;
        Content = content;
    }
}