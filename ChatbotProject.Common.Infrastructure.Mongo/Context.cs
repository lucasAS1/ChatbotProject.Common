using System.Diagnostics.CodeAnalysis;
using ChatbotProject.Common.Domain.Models.Settings;
using ChatbotProject.Common.Infrastructure.Mongo.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;

namespace ChatbotProject.Common.Infrastructure.Mongo;

[ExcludeFromCodeCoverage]
public class Context : IContext
{
    private readonly IMongoDatabase _database;

    public Context(IOptions<ApiSettings> config)
    {
        var mongoSettings = config.Value.MongoDbSettings;
        var client = CreateMongoClientSettings(mongoSettings);

        _database = client.GetDatabase(mongoSettings.Database);
    }

    public IMongoCollection<TDocument> GetCollection<TDocument>(string entityName)
    {
        return _database.GetCollection<TDocument>(entityName);
    }

    private static MongoClient CreateMongoClientSettings(MongoDbSettings mongoSettings)
    {
        var mongoClientSettings = CreateConnectionSettings(mongoSettings);

        return new MongoClient(mongoClientSettings);
    }

    private static MongoClientSettings CreateConnectionSettings(MongoDbSettings mongoSettings)
    {
        var mongoClientSettings = mongoSettings.Url is not null
            ? MongoClientSettings.FromConnectionString(mongoSettings.Url)
            : new MongoClientSettings()
            {
                Scheme = ConnectionStringScheme.MongoDBPlusSrv,
                Server = new MongoServerAddress(mongoSettings.Host, mongoSettings.Port),
                ConnectTimeout = mongoSettings.Timeout,
                UseTls = mongoSettings.UseTls,
                RetryWrites = mongoSettings.RetryWrites
            };
        
        mongoClientSettings.ServerApi =
            mongoSettings.ServerVersionSpecified ? new ServerApi(ServerApiVersion.V1) : null;
        AddCredentialIfExists(mongoSettings, mongoClientSettings);
        
        return mongoClientSettings;
    }

    private static void AddCredentialIfExists(MongoDbSettings mongoSettings, MongoClientSettings mongoClientSettings)
    {
        if (!String.IsNullOrEmpty(mongoSettings.User))
        {
            mongoClientSettings.Credential = MongoCredential.CreateCredential(mongoSettings.Database, mongoSettings.User, mongoSettings.Password);
        }
    }
}