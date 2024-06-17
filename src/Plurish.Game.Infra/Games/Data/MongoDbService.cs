using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Plurish.Game.Infra.Games.Data;
public class MondoDbService
{
    private readonly IConfiguration _configuration;
    private readonly IMongoDatabase? _database;

    public MondoDbService(IConfiguration configuration)
    {
        _configuration = configuration;

        var connectionString = _configuration.GetConnectionString("DbConnection");
        var mongoUrl = MongoUrl.Create(connectionString);
        var mongoClient = new MongoClient(mongoUrl);
        _database = mongoClient.GetDatabase(mongoUrl.DatabaseName);
    }

    public IMongoDatabase? Database => _database;
}
