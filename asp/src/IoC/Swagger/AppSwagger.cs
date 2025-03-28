using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;

namespace IoC.Swagger;

// Insere as configurações do swagger
public static class AppSwagger
{
    public static WebApplication AddSwaggerConf(this WebApplication app, string deployUrl)
    {
        app.UseSwagger(options =>
        {
            options.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
            {
                swaggerDoc.Servers = new List<OpenApiServer>
                {
                    new OpenApiServer { Url = deployUrl }
                };
            });
        });
        app.UseSwaggerUI();
        return app;
    }
}
