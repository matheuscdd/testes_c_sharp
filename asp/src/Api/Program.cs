using System.Net;
using System.Text.Json.Serialization;
using Application.Contexts.Users.Repositories;
using Domain.Exceptions;
using Mapster;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Repository.Context;
using Repository.Repositories.Users;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// personaliza as exceções
builder.Services.AddProblemDetails();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

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

// Configuração para colocar no swagger
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Serve para mostrar o Enum como strings no Swagger
builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
);

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapScalarApiReference();
app.MapControllers();

// personaliza as exceções
app.UseStatusCodePages();
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