using Application.Contexts.Users.Dtos;
using MediatR;

namespace Application.Contexts.Users.Queries.GetById;

public class GetUserByIdQuery: IRequest<UserDto?>
{
    public int Id { get; set; }
    public GetUserByIdQuery(int id)
    {
        Id = id;
    }

    public GetUserByIdQuery() {}
}