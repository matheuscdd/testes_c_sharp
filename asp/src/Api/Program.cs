using Api.Services;
using Api.Middlewares;
using Domain.Services;
using IoC.SigNoz; 
using IoC.Swagger;
using IoC.Identity;
using IoC.Jwt;
using IoC.Mapster;
using IoC.MediatR;
using IoC.Exceptions;
using IoC.Controllers;
using IoC.Repositories;
using IoC.Database;


var builder = WebApplication.CreateBuilder(args);

// carrega variáveis de ambiente
var connectString = Environment.GetEnvironmentVariable("DefaultConnection") ?? throw new Exception("Connection_String cannot be empty");
var host = Environment.GetEnvironmentVariable("Host") ?? throw new Exception("Host cannot be empty");
var secretKey = Environment.GetEnvironmentVariable("SecretKey") ?? throw new Exception("SecretKey cannot be empty");
var deployUrl = Environment.GetEnvironmentVariable("DEPLOY_URL") ?? "http://localhost";

builder.Configuration["JWT:Issuer"] = host;
builder.Configuration["JWT:Audience"] = host;
builder.Configuration["JWT:SigningKey"] = secretKey;
builder.Configuration["ConnectionStrings:DefaultConnection"] = connectString;


builder
    .AddExceptionsConf() // Personaliza as exceções
    .AddDatabaseConf() // adiciona as configurações de conexão com o banco de dados
    .AddControllersConf() // Captura os controllers
    .AddMediatRConf() // Carrega o driver de acesso ao banco de dados com as models
    .AddIdentityConf() // Configuração de senha do identity
    .AddJwtConf() // Configuração do JWT
    .AddMapsterConf() // Adicionar o mapters que já configura os métodos de mapping Id = Id
    .AddSwaggerConf() // Configuração para colocar no swagger
    .AddRepositoriesConf() // Adiciona a injeção de dependências nos repositórios
    .Services.AddScoped<ITokenService, TokenService>() // Estabelece as configurações para geração do token
;

if (builder.Environment.EnvironmentName.Equals("Production"))
{
    builder.AddSigNozConf(); // Configuração signoz
}

var app = builder.Build();

app
    .AddExceptionsConf() // personaliza as exceções
    .AddSwaggerConf(deployUrl) // configurações da documentação
    .AddControllersConf() // Mapeia os controllers para serem acessíveis via rota
    .UseMiddleware<TokenValidationMiddleware>() // Registra os middlewares
;

app.Run();