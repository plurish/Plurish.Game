using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Plurish.Common.Types.Output;
using Plurish.Game.Api.Filters.ResponseMapping;
using Plurish.Game.Application.Games;
using Plurish.Game.Domain.Games.Dtos;

namespace Plurish.Game.Api.Controllers;
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{v:apiVersion}/games")]
[ProducesResponseType(typeof(Response<>), StatusCodes.Status500InternalServerError)]
public sealed class GamesController(IGameService service) : ControllerBase
{
    readonly IGameService _service = service;

    /// <summary>
    /// Buscar jogos
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(Response<GameDto[]>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<>), StatusCodes.Status400BadRequest)]
    public Task<Result<List<GameDto>?>> Buscar() => _service.Buscar();

    /// <summary>
    /// Buscar jogo por UUID
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(Response<GameDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    public Task<Result<GameDto?>> Buscar([FromRoute] Guid id) => _service.Buscar(id);

    /// <summary>
    /// Publicar novo jogo
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(void), StatusCodes.Status201Created)]
    public Task Publicar([FromBody] GameDto game) => _service.Publicar(game);

    /// <summary>
    /// Editar jogo
    /// </summary>
    [HttpPatch]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    public Task Editar([FromBody] GameDto game) => _service.Editar(game);

    /// <summary>
    /// Deletar jogo
    /// </summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    public Task Deletar([FromRoute] Guid id) => _service.Deletar(id);
}
