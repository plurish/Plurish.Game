using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Plurish.Common.Types.Output;
using Plurish.Game.Api.Filters.ResponseMapping;
using Plurish.Game.Application.Games.Abstractions;
using Plurish.Game.Application.Games.Dtos;
using Plurish.Game.Application.Tempos.Abstractions;
using Plurish.Game.Application.Tempos.Dtos;
using Plurish.Game.Domain.Games.Dtos;
using Plurish.Game.Domain.Tempos.Dtos;

namespace Plurish.Game.Api.Controllers;
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{v:apiVersion}/games")]
[ProducesResponseType(typeof(Response<>), StatusCodes.Status500InternalServerError)]
[ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
public sealed class GameController(IGameService service) : ControllerBase
{
    readonly IGameService _service = service;

    /// <summary>
    /// Busca todos os jogos no banco
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(Response<GameDto[]>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<>), StatusCodes.Status400BadRequest)]
    public Task<Result<GameDto[]?>> BuscarGames() =>
        _service.BuscarGames();

    /// <summary>
    /// Busca um jogo no banco pelo id
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Response<GameDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<>), StatusCodes.Status400BadRequest)]
    public Task<Result<GameDto?>> BuscarGame([FromRoute] int id) =>
        _service.BuscarGame(id);

    /// <summary>
    /// Posta um novo jogo no banco
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(Response<GameDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<>), StatusCodes.Status400BadRequest)]
    public Task<Result<GameDto?>> PostarGame([FromBody] GameDto game) =>
        _service.PostarGame(game);
}
