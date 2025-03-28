using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace IoC.MediatR;


public static class BuilderMediatR
{
     public static WebApplicationBuilder AddMediatRConf(this WebApplicationBuilder builder)
     {
        builder.Services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssembly(Application.AssemblyReference
               .GetAssembly());
        });


        return builder;
     }
}