using Microsoft.Extensions.Logging;
using Plurish.Common.Types.Output;
using Plurish.Game.Domain.Games.Abstractions;
using Plurish.Game.Domain.Games.Dtos;

namespace Plurish.Game.Application.Games;

internal sealed class GameService(IGameRepository gameRepository, ILogger<GameService> logger) : IGameService
{
    readonly IGameRepository _repository = gameRepository;
    readonly ILogger<GameService> _logger = logger;

    public async Task<Result<List<GameDto>?>> Buscar()
    {
        List<GameDto>? games = await _repository.Buscar();

        return Result<List<GameDto>?>.Ok(games ?? []);
    }

    public async Task<Result<GameDto?>> Buscar(Guid id)
    {
        GameDto? game = await _repository.Buscar(id);

        if (game is null)
        {
            _logger.LogInformation("[Buscar] Jogo não encontrado - Id: {Id}", id);

            return Result<GameDto?>.Empty;
        }

        return Result<GameDto?>.Ok(game);
    }

    public async Task<Result> Publicar(GameDto game)
    {
        var dto = game with { Id = Guid.NewGuid() };

        await _repository.Publicar(dto);

        return Result.Created(["Jogo publicado com sucesso!"]);
    }

    public async Task<Result> Deletar(Guid id)
    {
        await _repository.Deletar(id);

        return Result.Empty;
    }

    public async Task<Result> Editar(GameDto game)
    {
        await _repository.Editar(game);

        return Result.Empty;
    }
}
