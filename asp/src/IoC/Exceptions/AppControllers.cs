using Microsoft.AspNetCore.Builder;
using System.Diagnostics;
using System.Net;
using Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace IoC.Exceptions;

public static class AppExceptions {
  public static WebApplication AddExceptionsConf(this WebApplication app) {
    app.UseStatusCodePages();
    app.UseExceptionHandler(config => {
      config.Run(async context => {
        var logger = context.RequestServices.GetRequiredService<ILogger<WebApplicationBuilder>>();

        context.Response.ContentType = "application/json";
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
        if (exception is BaseException custom) {
          context.Response.StatusCode = custom.StatusCode;
          var problemDetails = new ProblemDetails {
            Type = custom.Type,
              Title = custom.Message,
              Status = custom.StatusCode,
              Extensions = {
                {
                  "traceId",
                  custom.TraceId
                }
              }
          };
          logger.LogError(exception, $"TraceId: {custom.TraceId} -> Handle exception: {exception?.Message}");

          await context.Response.WriteAsJsonAsync(problemDetails);
        } else {
          var traceId = Activity.Current?.Id ?? "N/A";
          context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
          var problemDetails = new ProblemDetails {
            Type = "https://tools.ietf.org/html/rfc9110#section-15.6.1",
              Title = "An error occurred while processing your request",
              Status = (int) HttpStatusCode.InternalServerError,
              Extensions = {
                {
                  "traceId",
                  traceId
                }
              }
          };
          logger.LogError(exception, $"TraceId: {traceId} -> Unhandled exception: {exception?.Message}");

          await context.Response.WriteAsJsonAsync(problemDetails);
        }

      });
    });

    return app;
  }
}