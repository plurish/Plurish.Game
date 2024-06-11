using Plurish.Common.Types.Output;
using Plurish.Game.Domain.Tempos.Dtos;

namespace Plurish.Game.Application.Tempos.Abstractions;

public interface ICidadeService
{
    Task<Result<CidadeDto?>> BuscarPorNome(string cidade);
}