using System.Net;
using Application.Contexts.Users.Repositories;
using Domain.Exceptions;
using Domain.Exceptions.Users;
using Mapster;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Repository.Repositories.Users;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// personaliza as exceções
builder.Services.AddProblemDetails();
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddScoped<IUserRepository, UserRepository>();

var cs = builder.Configuration.GetConnectionString("OnionDb");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(cs).EnableSensitiveDataLogging()
        .LogTo(Console.WriteLine, LogLevel.Information));

builder.Services.AddMediatR(options =>
{
    options.RegisterServicesFromAssembly(Application.AssemblyReference
        .GetAssembly());
});

builder.Services.AddMapster();


var app = builder.Build();

// app.MapOpenApi();
app.MapScalarApiReference();
app.MapControllers();

// personaliza as exceções
app.UseExceptionHandler(config => 
{
    config.Run(async context => 
    {
        context.Response.ContentType = "application/json";
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
        if (exception is BaseException custom)
        {
            context.Response.StatusCode = custom.StatusCode;
            var problemDetails = new ProblemDetails
            {
                Title = custom.Message,
                Status = custom.StatusCode,
            };

            await context.Response.WriteAsJsonAsync(problemDetails);
        }
        else
        {
            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            var problemDetails = new ProblemDetails
            {
                Title = "An error occurred while processing your request.",
                Status = (int) HttpStatusCode.InternalServerError,
            };

            await context.Response.WriteAsJsonAsync(problemDetails);
        }

    });
});


app.Run();