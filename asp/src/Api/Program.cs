using System.Diagnostics;
using System.Net;
using System.Text.Json.Serialization;
using api.Services;
using Api.Middlewares;
using Application.Contexts.Users.Repositories;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Services;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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

// Configuração de senha do identity
builder.Services.AddIdentity<User, IdentityRole>(options => {
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 12;
}).AddEntityFrameworkStores<ApplicationDbContext>();

// Configuração do JWT
builder.Services.AddAuthentication(options => 
    options.DefaultAuthenticateScheme = 
    options.DefaultChallengeScheme = 
    options.DefaultForbidScheme = 
    options.DefaultSignInScheme =
    options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme
).AddJwtBearer(options => 
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"]!)
        ),
    };

    options.Events = new JwtBearerEvents
    {
        OnChallenge = context =>
        {
            context.HandleResponse();
            var traceId = Activity.Current?.Id ?? context.HttpContext.TraceIdentifier; 

            var problemDetails = new ProblemDetails
            {
                Type = "https://tools.ietf.org/html/rfc9110#section-15.5.2",
                Title = "Unauthorized",
                Status = StatusCodes.Status401Unauthorized,
                Extensions = { { "traceId", traceId } }
            };

            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsJsonAsync(problemDetails);
        }
    };
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
builder.Services.AddScoped<ITokenService, TokenService>();

var app = builder.Build();

// Registra os middlewares
app.UseMiddleware<TokenValidationMiddleware>();

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
        // TODO depois testar com signoz
        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();

        context.Response.ContentType = "application/json";
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
        if (exception is BaseException custom)
        {
            context.Response.StatusCode = custom.StatusCode;
            var problemDetails = new ProblemDetails
            {
                Type = custom.Type,
                Title = custom.Message,
                Status = custom.StatusCode,
                Extensions = { { "traceId", custom.TraceId } }
            };
            logger.LogError(exception, "TraceId: {TraceId} - Handle exception: {Message}", custom.TraceId, exception?.Message);

            await context.Response.WriteAsJsonAsync(problemDetails);
        }
        else
        {            
            var traceId = Activity.Current?.Id ?? "N/A"; 
            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            var problemDetails = new ProblemDetails
            {
                Type = "https://tools.ietf.org/html/rfc9110#section-15.6.1",
                Title = "An error occurred while processing your request",
                Status = (int) HttpStatusCode.InternalServerError,
                Extensions = { { "traceId", traceId } }
            };
            logger.LogError(exception, "TraceId: {TraceId} - Unhandled exception: {Message}", traceId, exception?.Message);

            await context.Response.WriteAsJsonAsync(problemDetails);
        }

    });
});


app.Run();