using Plurish.Common.Configuration;

namespace Plurish.Game.Infra;

/// <summary>
/// Representa as principais configurações de infra do appsettings.json
/// </summary>
public static class Settings
{
    /// <summary>
    /// Seção de 'Database' do appsettings
    /// </summary>
    public sealed class Database
    {
        public required SqlOptions Gaming { get; init; }
    }

    /// <summary>
    /// Seção de 'Api' do appsettings
    /// </summary>
    public sealed class Api
    {
        public required ApiOptions Elasticsearch { get; init; }
    }
}