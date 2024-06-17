using Plurish.Common.Abstractions.Domain;
using Plurish.Game.Domain.Games.Dtos;

namespace Plurish.Game.Domain.Games.Models;
public sealed class Game(
    int id,
    string title,
    string cover,
    string backgroundImage,
    string description,
    string genre,
    string publisher,
    string developer,
    string platform,
    string[] videos,
    Uri gameUrl,
    DateTime releaseDate,
    string[] screenshots,
    SystemRequirementsDto systemRequirements
) : AggregateRoot<int>(id)
{
    public string Title { get; private set; } = title;
    public string Cover { get; private set; } = cover;
    public string BackgroundImage { get; private set; } = backgroundImage;
    public string Description { get; private set; } = description;
    public string Genre { get; private set; } = genre;
    public string Publisher { get; private set; } = publisher;
    public string Developer { get; private set; } = developer;
    public string Platform { get; private set; } = platform;
    public string[] Videos { get; private set; } = videos;
    public Uri GameUrl { get; private set; } = gameUrl;
    public DateTime ReleaseDate { get; private set; } = releaseDate;
    public string[] Screenshots { get; private set; } = screenshots;
    public SystemRequirementsDto SystemRequirements { get; private set; } = systemRequirements;

}
