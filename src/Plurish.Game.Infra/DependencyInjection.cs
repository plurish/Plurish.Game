using Mapster;
using MapsterMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Plurish.Game.Domain.Games.Abstractions;
using Plurish.Game.Infra.Games;
using Prometheus;

namespace Plurish.Game.Infra;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager config
    ) =>
        services
            .AddSettings(
                config,
                out Settings.Api api,
                out Settings.Database db
            )
            .AddHealthChecking(api, db)
            .AddRepositories()
            .AddMappers();

    private static IServiceCollection AddSettings(
        this IServiceCollection services,
        ConfigurationManager config,
        out Settings.Api api,
        out Settings.Database db
    )
    {
        services
            .Configure<Settings.Api>(config.GetSection("Api"))
            .Configure<Settings.Database>(config.GetSection("Database"));

        ServiceProvider provider = services.BuildServiceProvider();

        api = provider.GetRequiredService<IOptions<Settings.Api>>().Value;
        db = provider.GetRequiredService<IOptions<Settings.Database>>().Value;

        return services;
    }

    private static IServiceCollection AddHealthChecking(
        this IServiceCollection services,
        Settings.Api apiSettings,
        Settings.Database dbSettings
    )
    {
        services.AddHealthChecks()
            .AddMongoDb(
                name: "Gaming MongoDB",
                mongodbConnectionString: dbSettings.Gaming.ConnectionString,
                failureStatus: HealthStatus.Unhealthy,
                tags: ["db", "mongo"]
            )
            .AddElasticsearch(
                elasticsearchUri: apiSettings.Elasticsearch.Url,
                name: "Elasticsearch",
                failureStatus: HealthStatus.Degraded,
                tags: ["db", "elastic", "log"]
            )
            .ForwardToPrometheus();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services) =>
        services
            .AddSingleton<IGameRepository, GameRepository>();

    private static IServiceCollection AddMappers(this IServiceCollection services) =>
        services
            .AddSingleton(TypeAdapterConfig.GlobalSettings)
            .AddSingleton<IMapper, ServiceMapper>();
}