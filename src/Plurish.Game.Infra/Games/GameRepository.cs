using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Plurish.Game.Domain.Games.Abstractions;
using Plurish.Game.Domain.Games.Dtos;

namespace Plurish.Game.Infra.Games;

internal sealed class GameRepository : IGameRepository
{
    readonly IMongoCollection<GameDto> _collection;

    public GameRepository(IOptions<Settings.Database> db)
    {
        MongoUrl url = MongoUrl.Create(db.Value.Gaming.ConnectionString);

        _collection = new MongoClient(url)
            .GetDatabase(url.DatabaseName)
            .GetCollection<GameDto>("games");
    }

    public Task<List<GameDto>?> Buscar() =>
        _collection.Find(_ => true).ToListAsync();

    public Task<GameDto?> Buscar(Guid id) =>
        _collection
            .Find(g => g.Id == id)
            .FirstOrDefaultAsync();

    public Task Publicar(GameDto game) => _collection.InsertOneAsync(game);

    public Task Editar(GameDto game) =>
        _collection.ReplaceOneAsync(g => g.Id == game.Id, game);

    public Task Deletar(Guid id) =>
        _collection.DeleteOneAsync(g => g.Id == id);
}
