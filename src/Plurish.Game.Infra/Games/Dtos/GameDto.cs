using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Plurish.Game.Domain.Games.Dtos;

namespace Plurish.Game.Infra.Games.Dtos;
internal readonly record struct GameDto
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; init; }

    public string? Title { get; init; }

    public string? Cover { get; init; }

    [JsonPropertyName("background_image")]
    [BsonElement("background_image")]
    public string? BackgroundImage { get; init; }

    public string? Description { get; init; }

    public string? Genre { get; init; }

    public string? Publisher { get; init; }

    public string? Developer { get; init; }

    public string? Platform { get; init; }

    public string? Videos { get; init; }

    [JsonPropertyName("game_url")]
    [BsonElement("game_url")]
    public string? GameUrl { get; init; }

    [JsonPropertyName("release_date")]
    [BsonElement("release_date")]
    public string? ReleaseDate { get; init; }

    public string? Screenshots { get; init; }

    [JsonPropertyName("sys_requirements")]
    [BsonElement("sys_requirements")]
    public SystemRequirementsDto? SysRequirements { get; init; }
}
