using Plurish.Game.Domain.Tempos.Models;

namespace Plurish.Game.Domain.Tempos.Abstractions;

public interface ICidadeRepository
{
    Task<Cidade?> BuscarPorNome(string cidade);
}