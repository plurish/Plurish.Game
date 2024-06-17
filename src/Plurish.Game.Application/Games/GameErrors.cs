using Plurish.Common.Types.Output;
using Plurish.Game.Domain.Games.Dtos;

namespace Plurish.Game.Application.Games;

internal static class GameErrors
{
    internal static readonly Result<GameDto[]?> GamesNaoEncontrados =
        Result<GameDto[]?>.UnexpectedError(["Algum erro ocorreu ao tentar buscar os jogos"]);

    internal static readonly Result GameJaExiste =
        Result<GameDto?>.UnexpectedError(["Não foi possível postar o jogo, pois ela já existe"]);
}
