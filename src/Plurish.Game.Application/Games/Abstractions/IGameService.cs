using Plurish.Common.Types.Output;
using Plurish.Game.Domain.Games.Dtos;

namespace Plurish.Game.Application.Games.Abstractions;
public interface IGameService
{
    Task<Result<GameDto[]?>> BuscarGames();
    Task<Result<GameDto?>> BuscarGame(int id);
    Task<Result<GameDto?>> PostarGame(GameDto game);
}
