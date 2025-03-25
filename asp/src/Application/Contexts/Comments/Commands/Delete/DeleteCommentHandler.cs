using Application.Contexts.Comments.Repositories;
using MediatR;

namespace Application.Contexts.Comments.Commands.Delete;

public class DeleteCommentHandler: IRequestHandler<DeleteCommentCommand>
{
    private readonly ICommentRepository _commentRepository;

    public DeleteCommentHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task Handle(
        DeleteCommentCommand request,
        CancellationToken cancellationToken
    )
    {
        await _commentRepository.DeleteAsync(request.Id, cancellationToken);
    }
}