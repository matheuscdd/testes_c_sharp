using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace IoC.Controllers;

public static class BuilderControllers
{
     public static WebApplicationBuilder AddControllersConf(this WebApplicationBuilder builder)
     {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddControllers();

        return builder;
     }
}