using Plurish.Common.Types.Output;
using Plurish.Game.Application.Tempos.Dtos;
using Plurish.Game.Domain.Tempos.Dtos;

namespace Plurish.Game.Application.Tempos.Abstractions;

public interface ITempoService
{
    /// <summary>
    /// Busca o tempo de uma cidade, pelo seu nome
    /// </summary>
    /// <param name="cidade">Nome da cidade, em qualquer idioma</param>
    /// <returns>Eventual tempo</returns>
    Task<Result<TempoDto?>> BuscarPorCidade(string cidade);

    /// <summary>
    /// Diminui a temperatura de uma cidade, de acordo com a quantidade especificada em Celsius
    /// </summary>
    Task<Result> DiminuirTemperatura(DiminuirTemperaturaDto input);
}