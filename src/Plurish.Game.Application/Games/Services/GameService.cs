using MapsterMapper;
using Microsoft.Extensions.Logging;
using Plurish.Common.Abstractions.Domain.Events;
using Plurish.Common.Types.Output;
using Plurish.Game.Application.Games.Abstractions;
using Plurish.Game.Application.Games.Dtos;
using Plurish.Game.Application.Games.Errors;
using Plurish.Game.Application.Games.Services;
using Plurish.Game.Application.Tempos.Errors;
using Plurish.Game.Domain.Games.Abstractions;
using Plurish.Game.Domain.Games.Dtos;
using Plurish.Game.Domain.Games.Models;
using Plurish.Game.Domain.Tempos.Dtos;
using Plurish.Game.Domain.Tempos.Models;
using Plurish.Game.Domain.Tempos.Models.ValueObjects;

namespace Plurish.Game.Application.Games.Services;
internal sealed class GameService(
    IGameRepository gameRepository,
    IGameService gameService,
    ILogger<GameService> logger,
    IMapper mapper,
    IDomainEventDispatcher eventDispatcher
) : IGameService
{
    readonly IGameRepository _gameRepository = gameRepository;
    readonly IGameService _gameService = gameService;
    readonly ILogger<GameService> _logger = logger;
    readonly IMapper _mapper = mapper;
    /*
    readonly IDomainEventDispatcher _eventDispatcher = eventDispatcher;

    public async Task<Result<TempoDto?>> BuscarPorCidade(string cidade)
    {
        if (string.IsNullOrEmpty(cidade))
        {
            return TempoErrors.CidadeInvalida;
        }

        Result<CidadeDto?> cidadeResult = await _cidadeService.BuscarPorNome(cidade);

        if (!cidadeResult.HasValue)
        {
            return new(cidadeResult);
        }

        var tempo = await _gameRepository.BuscarGames(
            _mapper.Map<Cidade>(cidadeResult.Value!)
        );

        if (tempo is null)
        {
            _logger.LogError("[BuscarPorCidade] Tempos nulo - Cidade: {Cidade}", cidade);

            return TempoErrors.TempoNaoEncontrado;
        }

        var dto = _mapper.Map<TempoDto?>(tempo);

        return Result<TempoDto?>.Ok(dto);
    }

    public async Task<Result> DiminuirTemperatura(DiminuirTemperaturaDto input)
    {
        Result<TempoDto?> tempoDto = await BuscarPorCidade(input.Cidade);

        if (!tempoDto.HasValue)
        {
            return new(tempoDto);
        }

        Tempo tempo = tempoDto.Value!.ParaDomain();

        Result result = tempo.DiminuirTemperatura(input.CelsiusDiminuidos);

        if (result.IsFailure)
        {
            _logger.LogError("Ocorreu um erro ao tentar diminuir a temperatura - Result: {@Result}", result);

            return new(result);
        }

        _eventDispatcher.Dispatch(tempo.PopEvents());

        return Result.Empty;
    }
    */

    public async Task<Result<GameDto[]?>> BuscarGames()
    {
        Result<GameDto[]?> gamesResult = await _gameService.BuscarGames();

        if (!gamesResult.HasValue)
        {
            return new(gamesResult);
        }

        var games = await _gameRepository.BuscarGames();

        if (games is null)
        {
            _logger.LogError("[BuscarGames] Jogos não encontrados");

            return GameErrors.GamesNaoEncontrados;
        }

        var dto = _mapper.Map<GameDto[]>(games);

        return Result<GameDto[]?>.Ok(dto);
    }

    public async Task<Result<GameDto?>> BuscarGame(int id)
    {
        Result<GameDto?> gameResult = await _gameService.BuscarGame(id);

        if (!gameResult.HasValue)
        {
            return new(gameResult);
        }

        var game = await _gameRepository.BuscarGame(id);

        if (game is null)
        {
            _logger.LogError("[BuscarGame] Jogo não encontrado - id: {@Id}", id);

            return GameErrors.GameNaoEncontrado;
        }

        var dto = _mapper.Map<GameDto>(game);

        return Result<GameDto?>.Ok(dto);
    }

    public async Task<Result<GameDto?>> PostarGame(Game game)
    {
        var convertedGame = _mapper.Map<GameDto?>(game);

        Result<GameDto?> gamesResult = await _gameService.PostarGame(convertedGame!);

        if (!gamesResult.IsFailure)
        {
            return new(gamesResult);
        }

        var newGame = await _gameRepository.PostarGame(game);

        if (newGame is not null)
        {
            _logger.LogError("[PostarGame] Não foi possível postar o jogo - Jogo: {@Game}", new);

            return (Result<GameDto?>)GameErrors.GameJaExiste;
        }

        convertedGame = _mapper.Map<GameDto?>(newGame!);

        return Result<GameDto?>.Ok(convertedGame);
    }
}
