using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Repository.Context;

namespace IoC.Identity;

public static class BuilderIdentity
{
     public static WebApplicationBuilder AddIdentityConf(this WebApplicationBuilder builder)
     {
       builder.Services.AddIdentity<User, IdentityRole>(options => {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequiredLength = 12;
        }).AddEntityFrameworkStores<ApplicationDbContext>();

        return builder;
     }
}