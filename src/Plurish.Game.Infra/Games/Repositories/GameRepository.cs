
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Plurish.Game.Domain.Games.Abstractions;
using Plurish.Game.Infra.Games.Data;
using Plurish.Game.Domain.Games.Models;

namespace Plurish.Game.Infra.Games.Repositories;
internal sealed class GameRepository(
    
    IOptionsMonitor<Settings.Api> apis
    ) : IGameRepository
{
    private readonly IMongoCollection<Game> _games;

    public GameRepository(MondoDbService mongoDbService)
    {
        _games = mongoDbService.Database?.GetCollection<Game>("games");
    }

    public async Task<IEnumerable<Game>> BuscarGames()
    {
        return await _games.Find(FilterDefinition<Game>.Empty).ToListAsync();
    }

    public async Task<Game?> BuscarGame(int id)
    {
        var filter = Builders<Game>.Filter.Eq("_id",id);
        var game = await _games.Find(filter).FirstOrDefaultAsync();
        return game;
    }

    public async Task<Game?> PostarGame(Game game)
    {
        await _games.InsertOneAsync(game);
        return game;
    }
}
