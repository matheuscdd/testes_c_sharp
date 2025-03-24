using Application.Contexts.Users.Repositories;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Repository.Repositories.Users;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

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


app.Run();