using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Repository.Repositories.Comments;
using Repository.Repositories.Portfolios;
using Repository.Repositories.Stocks;
using Repository.Repositories.Users;
using Application.Contexts.Comments.Repositories;
using Application.Contexts.Portfolios.Repositories;
using Application.Contexts.Stocks.Repositories;
using Application.Contexts.Users.Repositories;

namespace IoC.Repositories;

public static class BuilderMapster
{
     public static WebApplicationBuilder AddRepositoriesConf(this WebApplicationBuilder builder)
     {
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IStockRepository, StockRepository>();
        builder.Services.AddScoped<ICommentRepository, CommentRepository>();
        builder.Services.AddScoped<IPortfolioRepository, PortfolioRepository>();

        return builder;
     }
}