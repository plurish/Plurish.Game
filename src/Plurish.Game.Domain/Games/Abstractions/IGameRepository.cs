using Plurish.Game.Domain.Games.Models;

namespace Plurish.Game.Domain.Games.Abstractions;
public interface IGameRepository
{
    Task<Models.Game[]?> BuscarGames();
    Task<Models.Game?> BuscarGame(int id);
    Task<Models.Game?> PostarGame(Models.Game game);
}
