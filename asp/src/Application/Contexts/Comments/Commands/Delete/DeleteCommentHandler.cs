using Application.Contexts.Comments.Repositories;
using Domain.Exceptions;
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
        var entity = await _commentRepository.GetByIdAndUserAsync(request.Id, request.UserId, cancellationToken);
        if (entity == null)
        {
            throw new NotFoundCustomException("Comment not found");
        }

        await _commentRepository.DeleteAsync(entity, cancellationToken);
    }
}