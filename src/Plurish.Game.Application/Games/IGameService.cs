using Plurish.Common.Types.Output;
using Plurish.Game.Domain.Games.Dtos;

namespace Plurish.Game.Application.Games;

public interface IGameService
{
    Task<Result<List<GameDto>?>> Buscar();
    Task<Result<GameDto?>> Buscar(Guid id);
    Task<Result> Publicar(GameDto game);
    Task<Result> Editar(GameDto game);
    Task<Result> Deletar(Guid id);
}
