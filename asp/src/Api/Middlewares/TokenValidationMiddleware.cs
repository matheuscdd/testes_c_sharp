using System.Diagnostics;
using System.Security.Claims;
using Application.Contexts.Users.Repositories;
using Domain.Exceptions.Users;
using Microsoft.AspNetCore.Mvc;

namespace Api.Middlewares;

public class TokenValidationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IServiceScopeFactory _scopeFactory;

    public TokenValidationMiddleware(RequestDelegate next, IServiceScopeFactory scopeFactory)
    {
        _next = next;
        _scopeFactory = scopeFactory;
    }

    public async Task Invoke(HttpContext context)
    {
        var userId = context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!string.IsNullOrEmpty(userId))
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
                var exists = await userRepository.CheckIdExists(userId);
                if (!exists) 
                {
                    var traceId = Activity.Current?.Id ?? "N/A"; 

                    var problemDetails = new ProblemDetails
                    {
                        Type = "https://tools.ietf.org/html/rfc9110#section-15.5.2",
                        Title = "Unauthorized",
                        Status = StatusCodes.Status401Unauthorized,
                        Extensions = { { "traceId", traceId } }
                    };

                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsJsonAsync(problemDetails);
                    return;
                }
            }
        }

        await _next(context);
    }
}