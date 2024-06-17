using Plurish.Game.Domain.Games.Dtos;

namespace Plurish.Game.Domain.Games.Abstractions;

public interface IGameRepository
{
    Task<List<GameDto>?> Buscar();
    Task<GameDto?> Buscar(Guid id);
    Task Publicar(GameDto game);
    Task Editar(GameDto game);
    Task Deletar(Guid id);
}
