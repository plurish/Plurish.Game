using Plurish.Common.Types.Output;
using Plurish.Game.Domain.Tempos.Dtos;

namespace Plurish.Game.Application.Tempos.Errors;

internal static class CidadeErrors
{
    internal static readonly Result<CidadeDto?> InputInvalido =
        Result<CidadeDto?>.InvalidInput(["Nome da cidade não preenchido corretamente"]);

    internal static readonly Result<CidadeDto?> CidadeInexistente =
        Result<CidadeDto?>.Empty;
}