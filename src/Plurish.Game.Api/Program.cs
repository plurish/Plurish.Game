using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Plurish.Common;
using Plurish.Game.Api;
using Plurish.Game.Application;
using Plurish.Game.Infra;
using Presentation.Middleware;
using Prometheus;
using Serilog;

#pragma warning disable S1199

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddCommon(builder.Configuration)
        .AddPresentation(builder.Configuration)
        .AddInfrastructure(builder.Configuration)
        .AddApplication();

    builder.Host.UseSerilog((context, config) =>
        config.ReadFrom.Configuration(context.Configuration)
    );
}

WebApplication app = builder.Build();
{
    app.UseResponseCompression();

    app
        .UseSwagger(o => o.RouteTemplate = "api/{documentName}/swagger.{json|yaml}")
        .UseSwaggerUI(o =>
        {
            o.SwaggerEndpoint("/api/v1/swagger.json", "API Game v1");
            o.InjectStylesheet("/swagger.css");
            o.RoutePrefix = "docs";
        });

    if (!app.Environment.IsProduction())
    {
        app.UseDeveloperExceptionPage();
    }

    app
        .UseRouting()
        .UseStaticFiles()
        .UseMiddleware<ExceptionHandlingMiddleware>()
        .UseMiddleware<AuthMiddleware>();

    app
        .UseHealthChecks("/_health", new HealthCheckOptions()
        {
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        })
        .UseEndpoints(endpoints =>
        {
            endpoints.MapHealthChecksUI(o =>
            {
                o.UIPath = "/dashboard";
                o.PageTitle = "Health | Plurish-Api-Game";
                //o.AddCustomStylesheet("wwwroot/health.css");
            });
            endpoints.MapMetrics("/_metrics");
            endpoints.MapControllers();
        });

    app.Run();
}