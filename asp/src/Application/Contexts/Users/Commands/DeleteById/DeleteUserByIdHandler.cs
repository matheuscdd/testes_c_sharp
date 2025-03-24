using Application.Contexts.Users.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Contexts.Users.Commands.DeleteById;

public class DeleteUserByIdHandler: IRequestHandler<DeleteUserByIdCommand>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserByIdHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Handle(DeleteUserByIdCommand request,
    CancellationToken cancellationToken)
    {
        await _userRepository.DeleteAsync(request.Id, cancellationToken);
    }
}