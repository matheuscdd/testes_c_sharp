using Application.Contexts.Users.Dtos;
using MediatR;

namespace Application.Contexts.Users.Queries.GetById;

public class GetByIdUserQuery: IRequest<UserDto?>
{
    public required string Id { get; set; }
    public GetByIdUserQuery(string id)
    {
        Id = id;
    }

    public GetByIdUserQuery() {}
}