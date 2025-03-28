using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace IoC.Exceptions;

public static class BuilderExceptions
{
     public static WebApplicationBuilder AddExceptionsConf(this WebApplicationBuilder builder)
     {
        builder.Services.AddProblemDetails();

        return builder;
     }
}