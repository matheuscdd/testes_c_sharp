using Domain.Entities;

namespace Domain.Services;

public interface ITokenService
{
    string CreateToken(User entity);
}

