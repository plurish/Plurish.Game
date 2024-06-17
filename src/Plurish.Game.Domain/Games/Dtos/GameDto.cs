using System.Text.Json.Serialization;

namespace Plurish.Game.Domain.Games.Dtos;
public sealed record GameDto(
    Guid Id,
    string Title,
    string Cover,
    [property: JsonPropertyName("background_image")] string BackgroundImage,
    string Description,
    string Genre,
    string Publisher,
    string Developer,
    string Platform,
    string[] Videos,
    [property: JsonPropertyName("game_url")] Uri GameUrl,
    [property: JsonPropertyName("release_date")] DateTime ReleaseDate,
    string[] Screenshots,
    [property: JsonPropertyName("sys_requirements")] SystemRequirementsDto SystemRequirements
);
