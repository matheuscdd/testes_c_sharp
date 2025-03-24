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

// carrega variáveis de ambiente
var connectString = Environment.GetEnvironmentVariable("DefaultConnection") ?? throw new Exception("Connection_String cannot be empty");
var host = Environment.GetEnvironmentVariable("Host") ?? throw new Exception("Host cannot be empty");
var secretKey = Environment.GetEnvironmentVariable("SecretKey") ?? throw new Exception("SecretKey cannot be empty");

builder.Configuration["JWT:Issuer"] = host;
builder.Configuration["JWT:Audience"] = host;
builder.Configuration["JWT:SigningKey"] = secretKey;
builder.Configuration["ConnectionStrings:DefaultConnection"] = connectString;


// Personaliza as exceções
builder.Services.AddProblemDetails();

// Captura os controllers
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

// Carrega o driver de acesso ao banco de dados com as models
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")).EnableSensitiveDataLogging()
        .LogTo(Console.WriteLine, LogLevel.Information));

// Facilita a injeção e desacomplamento de dependências
builder.Services.AddMediatR(options =>
{
    options.RegisterServicesFromAssembly(Application.AssemblyReference
        .GetAssembly());
});

// Adicionar o mapters que já configura os métodos de mapping Id = Id
builder.Services.AddMapster();

// Configuração para colocar no swagger
builder.Services.AddSwaggerGen();
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

// Dependências a serem injetadas
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

// Insere o swagger em desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Faz a conversão direta entre Classe pra Dto
app.MapScalarApiReference();

// Mapeia os controllers para serem acessíveis via rota
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