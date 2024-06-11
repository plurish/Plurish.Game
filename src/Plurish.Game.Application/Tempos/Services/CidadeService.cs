using MapsterMapper;
using Microsoft.Extensions.Logging;
using Plurish.Common.Types.Output;
using Plurish.Game.Application.Tempos.Abstractions;
using Plurish.Game.Application.Tempos.Errors;
using Plurish.Game.Domain.Tempos.Abstractions;
using Plurish.Game.Domain.Tempos.Dtos;
using Plurish.Game.Domain.Tempos.Models;

namespace Plurish.Game.Application.Tempos.Services;

internal sealed class CidadeService(
    ICidadeRepository repository,
    IMapper mapper,
    ILogger<CidadeService> logger
) : ICidadeService
{
    readonly ICidadeRepository _repository = repository;
    readonly IMapper _mapper = mapper;
    readonly ILogger<CidadeService> _logger = logger;

    public async Task<Result<CidadeDto?>> BuscarPorNome(string cidade)
    {
        if (string.IsNullOrEmpty(cidade))
        {
            return CidadeErrors.InputInvalido;
        }

        Cidade? entity = await _repository.BuscarPorNome(cidade);

        if (entity is null)
        {
            if (_logger.IsEnabled(LogLevel.Warning))
            {
                _logger.LogWarning("A cidade {Cidade} não existe", cidade);
            }

            return CidadeErrors.CidadeInexistente;
        }

        var dto = _mapper.Map<CidadeDto>(entity);

        return Result<CidadeDto?>.Ok(dto);
    }
}