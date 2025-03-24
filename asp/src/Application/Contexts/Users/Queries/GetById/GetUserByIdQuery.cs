using Application.Contexts.Users.Dtos;
using MediatR;

namespace Application.Contexts.Users.Queries.GetById;

public class GetUserByIdQuery: IRequest<UserDto?>
{
    public string Id { get; set; }
    public GetUserByIdQuery(string id)
    {
        Id = id;
    }

    public GetUserByIdQuery() {}
}