using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;


namespace IoC.Database;

public static class BuilderDatabase
{
     public static WebApplicationBuilder AddDatabaseConf(this WebApplicationBuilder builder)
     {
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
         options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")).EnableSensitiveDataLogging()
            .LogTo(Console.WriteLine, LogLevel.Information)
        );

        return builder;
     }
}