using Microsoft.AspNetCore.Builder;

namespace IoC.Controllers;

public static class AppControllers
{
    public static WebApplication AddControllersConf(this WebApplication app)
    {
        app.MapControllers();
    
        return app;
    }
}
