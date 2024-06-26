﻿using Plurish.Game.Domain.Tempos.Models;
using Plurish.Game.Domain.Tempos.Models.ValueObjects;
using Plurish.Game.Infra.Tempos.Dtos.WeatherResponse;

namespace Plurish.Game.Infra.Tempos;

internal static class TempoMapper
{
    internal static Tempo ParaTempo(
        this WeatherResponseDto response,
        Cidade cidade
    )
    {
        WeatherDto? weather = response.Weather?.Length > 0
            ? response.Weather[0]
            : null;

        return new(
            weather?.Id ?? 0,
            weather?.Description ?? string.Empty,
            Temperatura.Criar(response.Main.Temperature).Value!,
            Temperatura.Criar(response.Main.FeelsLike).Value!,
            response.Main.Humidity,
            cidade
        );
    }
}