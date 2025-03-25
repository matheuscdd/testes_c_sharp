using Application.Contexts.Users.Dtos;
using MediatR;

namespace Application.Contexts.Users.Queries.GetAll;

public class GetAllUserQuery: IRequest<IReadOnlyCollection<UserDto>> 
{
    public GetAllUserQuery() {}
}