namespace Plurish.Game.Domain.Games.Dtos;
public readonly record struct SystemRequirementsDto(
    string OS,
    string Processor,
    string Memory,
    string Graphics,
    string Storage
);
