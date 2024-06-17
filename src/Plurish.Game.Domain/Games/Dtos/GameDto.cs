namespace Plurish.Game.Domain.Games.Dtos;
public sealed record GameDto(
    int Id,
    string Title,
    string Cover,
    string BackgroundImage,
    string Description,
    string Genre,
    string Publisher,
    string Developer,
    string Platform,
    string[] Videos,
    Uri GameUrl,
    DateTime ReleaseDate,
    string[] Screenshots,
    SystemRequirementsDto SystemRequirements
);
