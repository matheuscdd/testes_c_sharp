using Application.Contexts.Users.Repositories;
using Domain.Entities;
using Domain.Exceptions.Users;
using MediatR;

namespace Application.Contexts.Users.Commands.Delete;

public class DeleteUserByIdHandler: IRequestHandler<DeleteUserCommand>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserByIdHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Handle(DeleteUserCommand request,
    CancellationToken cancellationToken)
    {
        await _userRepository.DeleteAsync(request.Id, cancellationToken);
    }
}