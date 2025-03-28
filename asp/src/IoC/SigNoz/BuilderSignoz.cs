using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using OpenTelemetry.Exporter;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Logging;

namespace IoC.SigNoz;

public static class BuilderSigNoz
{
     public static WebApplicationBuilder AddSigNozConf(this WebApplicationBuilder builder)
     {
        var resourceBuilder = ResourceBuilder.CreateDefault().AddService("project-asp");
        var protocol = OtlpExportProtocol.Grpc;
        var signozUrl = new Uri(Environment.GetEnvironmentVariable("SignozUrl")!);

        builder.Logging.ClearProviders();
        builder.Logging.AddOpenTelemetry(options =>
        {
            options.IncludeScopes = true;
            options
                .SetResourceBuilder(resourceBuilder)
                .AddOtlpExporter(otlpOptions =>
                {
                    otlpOptions.Endpoint = signozUrl;
                    otlpOptions.Protocol = protocol;
                });
        });
        builder.Services.AddOpenTelemetry()
            .WithTracing(tracing => tracing
                .SetResourceBuilder(resourceBuilder)
                .AddAspNetCoreInstrumentation()
                .AddOtlpExporter(otlpOptions =>
                {
                    otlpOptions.Endpoint = signozUrl;
                    otlpOptions.Protocol = protocol;
                }))
            .WithMetrics(metrics => metrics
                .AddAspNetCoreInstrumentation()
                .AddOtlpExporter(otlpOptions =>
                {
                    otlpOptions.Endpoint = signozUrl;
                    otlpOptions.Protocol = protocol;
                }));

        return builder;
     }
}