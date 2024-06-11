using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Plurish.Common.Types.Output;
using Plurish.Game.Api.Filters.ResponseMapping;
using Plurish.Game.Application.Tempos.Abstractions;
using Plurish.Game.Application.Tempos.Dtos;
using Plurish.Game.Domain.Tempos.Dtos;

namespace Plurish.Game.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{v:apiVersion}/tempos")]
[ProducesResponseType(typeof(Response<>), StatusCodes.Status500InternalServerError)]
[ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
public sealed class TemposController(ITempoService service) : ControllerBase
{
    readonly ITempoService _service = service;

    /// <summary>
    /// Busca o tempo atual da cidade
    /// </summary>
    [HttpGet("{cidade}")]
    [ProducesResponseType(typeof(Response<TempoDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<>), StatusCodes.Status400BadRequest)]
    public Task<Result<TempoDto?>> BuscarPorCidade([FromRoute] string cidade) =>
        _service.BuscarPorCidade(cidade);

    /// <summary>
    /// Diminui a temperatura de uma cidade, na quantidade especificada em Celsius (sqn)
    /// </summary>
    [HttpPatch("diminuir-temperatura")]
    public Task<Result> DiminuirTemperatura([FromBody] DiminuirTemperaturaDto request) =>
        _service.DiminuirTemperatura(request);
}