using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Application.Mappings;
using Mapster;
using MapsterMapper;

namespace IoC.Mapster;

public static class BuilderMapster
{
     public static WebApplicationBuilder AddMapsterConf(this WebApplicationBuilder builder)
     {
        builder.Services.AddMapster();
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(typeof(CommentMappingConfig).Assembly);
        builder.Services.AddSingleton(config);
        builder.Services.AddScoped<IMapper, ServiceMapper>();  

        return builder;
     }
}