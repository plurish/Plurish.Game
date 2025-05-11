using Asp.Versioning;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Plurish.Common.Configuration;
using Plurish.Common.Types.Output;
using Plurish.Game.Api.Filters;
using Plurish.Game.Api.Filters.ResponseMapping;
using Presentation.Middleware;

namespace Plurish.Game.Api;

internal static class DependencyInjection
{
    public static IServiceCollection AddPresentation(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddSettings(configuration, out AuthOptions authSettings);

        return services
            .AddHealthCheckUI(authSettings)
            .AddSwagger()
            .AddResponseCompression()
            .AddVersioning()
            .AddMiddlewares()
            .AddMappers();
    }

    private static IServiceCollection AddSettings(
        this IServiceCollection services,
        IConfiguration configuration,
        out AuthOptions authSettings
    )
    {
        services
            .Configure<AuthOptions>(configuration.GetSection("Auth"));

        authSettings = services
            .BuildServiceProvider()
            .GetRequiredService<IOptions<AuthOptions>>()
            .Value;

        return services;
    }

    private static IServiceCollection AddVersioning(this IServiceCollection services)
    {
        services
            .AddApiVersioning(o =>
            {
                o.DefaultApiVersion = new ApiVersion(1);
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.ApiVersionReader = ApiVersionReader.Combine(
                    new UrlSegmentApiVersionReader(),
                    new HeaderApiVersionReader("version", "x-version")
                );
            })
            .AddApiExplorer(o =>
            {
                o.GroupNameFormat = "'v'V";
                o.SubstituteApiVersionInUrl = true;
            });

        return services;
    }

    private static IServiceCollection AddHealthCheckUI(
        this IServiceCollection services,
        AuthOptions authSettings
    )
    {
        services
            .AddHealthChecksUI(o => o.ConfigureApiEndpointHttpclient((s, client) =>
                client.DefaultRequestHeaders.Add(
                    "api-key",
                    authSettings.ApiKeys["Plurish-Api-Gaming"]
                )
            ))
            .AddInMemoryStorage();

        return services;
    }

    private static IServiceCollection AddMiddlewares(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.Filters.Add<LoggingFilter>();
            options.Filters.Add<ResponseMappingFilter>();
        });

        return services
            .AddTransient<AuthMiddleware>()
            .AddSingleton<ExceptionHandlingMiddleware>();
    }

    private static IServiceCollection AddMappers(this IServiceCollection services)
    {
        TypeAdapterConfig
            .GlobalSettings
            .ForType(typeof(Result<>), typeof(Response<>))
            .Map("Data", "Value");

        return services
            .AddSingleton(TypeAdapterConfig.GlobalSettings)
            .AddSingleton<IMapper, ServiceMapper>();
    }

    private static IServiceCollection AddSwagger(this IServiceCollection services) =>
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddSwaggerGen(o =>
        {
            o.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Plurish.Game",
                Description = "REST API responsável pelo gerenciamento dos Games do Plurish",
                Contact = new OpenApiContact
                {
                    Name = "Plurish Team",
                    Email = "plurish.dev@gmail.com",
                },
            });

            o.AddSecurityDefinition("ApiKey", new()
            {
                Description = "API key para autenticação",
                Type = SecuritySchemeType.ApiKey,
                Name = "api-key",
                In = ParameterLocation.Header,
                Scheme = "ApiKeyScheme"
            });

            o.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme()
                    {
                        Reference = new OpenApiReference()
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "ApiKey"
                        },
                        In = ParameterLocation.Header
                    },
                    new List<string>()
                }
            });
        });
}