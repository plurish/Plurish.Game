using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Plurish.Game.Domain.Games.Dtos;

namespace Plurish.Game.Infra.Games.Dtos;
internal readonly record struct GameDto
{
    [JsonPropertyName("_id")]
    public string? Id { get; init; }

    [JsonPropertyName("title")]
    public string? Title { get; init; }

    [JsonPropertyName("cover")]
    public string? Cover { get; init; }

    [JsonPropertyName("background_image")]
    public string? BackgroundImage { get; init; }

    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("genre")]
    public string? Genre { get; init; }

    [JsonPropertyName("publisher")]
    public string? Publisher { get; init; }

    [JsonPropertyName("developer")]
    public string? Developer { get; init; }

    [JsonPropertyName("platform")]
    public string? Platform { get; init; }

    [JsonPropertyName("videos")]
    public string? Videos { get; init; }

    [JsonPropertyName("game_url")]
    public string? GameUrl { get; init; }

    [JsonPropertyName("release_date")]
    public string? ReleaseDate { get; init; }

    [JsonPropertyName("screenshots")]
    public string? Screenshots { get; init; }

    [JsonPropertyName("sys_requirements")]
    public SystemRequirementsDto? SysRequirements { get; init; }
}
