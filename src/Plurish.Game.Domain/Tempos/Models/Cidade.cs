using Plurish.Common.Abstractions.Domain;
using Plurish.Game.Domain.Tempos.Models.ValueObjects;

namespace Plurish.Game.Domain.Tempos.Models;

public sealed class Cidade(CidadeId id, string nome) : Entity<CidadeId>(id)
{
    public string Nome { get; private set; } = nome;
}