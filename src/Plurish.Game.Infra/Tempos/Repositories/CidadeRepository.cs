﻿using Microsoft.Extensions.Options;
using Plurish.Game.Domain.Tempos.Abstractions;
using Plurish.Game.Domain.Tempos.Models;
using Plurish.Game.Domain.Tempos.Models.ValueObjects;
using Plurish.Game.Infra.Tempos.Dtos;
using Refit;

namespace Plurish.Game.Infra.Tempos.Repositories;

internal sealed class CidadeRepository(
    IOpenWeatherApiClient weatherApi,
    IOptionsMonitor<Settings.Api> apis
) : ICidadeRepository
{
    readonly IOpenWeatherApiClient _weatherApi = weatherApi;
    readonly string _apiToken = apis.CurrentValue.OpenWeather.Token;

    /// <summary>
    /// Busca uma cidade pelo seu nome. 
    /// Caso existam várias de mesmo nome, retorna a primeira encontrada
    /// </summary>
    /// <param name="cidade"></param>
    /// <returns>Eventual cidade</returns>
    /// <exception cref="ApiException">Lançada quando a API não retorna status de sucesso</exception>
    public async Task<Cidade?> BuscarPorNome(string cidade)
    {
        CityDto[] response = await _weatherApi.BuscarCidades(
            _apiToken,
            cidade
        );

        if (response.Length < 1) return null;

        CityDto dto = response[0];

        return new Cidade(
            new CidadeId(dto.Latitude, dto.Longitude),
            dto.Name
        );
    }
}