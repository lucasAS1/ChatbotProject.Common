using ChatbotProject.Common.Domain.Models.Settings;
using ChatbotProject.Common.Infrastructure.Mongo.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ChatbotProject.Common.Infrastructure.Mongo;

public class Repository<TDocument> : IRepository<TDocument> where TDocument : BaseEntity
{
    private readonly IMongoCollection<TDocument> _collection;
    
    public Repository(IOptions<ApiSettings> config)
    {
        var mongoUrl = config.Value.MongoDbSettings.Url;
        var database = config.Value.MongoDbSettings.Database;
        var mongoClient = new MongoClient(mongoUrl);
        mongoClient.StartSession();

        var mongoDatabase = mongoClient.GetDatabase(database);
        _collection = mongoDatabase.GetCollection<TDocument>(nameof(TDocument));
    }


    public async Task AddDocument(TDocument document)
    {
        await _collection.InsertOneAsync(document);
    }

    public async Task UpdateDocument(TDocument document)
    {
        await _collection.ReplaceOneAsync(
            new ExpressionFilterDefinition<TDocument>(x => x.Id == document.Id),
            document);
    }

    public async Task DeleteDocument(TDocument document)
    {
        var expressionFilterDefinition = new ExpressionFilterDefinition<TDocument>(x => x.Id == document.Id);
        
        await _collection.DeleteOneAsync(expressionFilterDefinition);
    }

    public async Task<TDocument> GetDocument(TDocument document)
    {
        var expressionFilterDefinition = new ExpressionFilterDefinition<TDocument>(x => x.Id == document.Id);
        
        var dbQueryResult = await _collection.FindAsync(expressionFilterDefinition);
        
        return dbQueryResult.FirstOrDefault();
    }

    public async Task<List<TDocument>> GetDocuments(TDocument document)
    {
        var expressionFilterDefinition = new ExpressionFilterDefinition<TDocument>(x => x.Id == document.Id);
        
        var dbQueryResult = await _collection.FindAsync(expressionFilterDefinition);
        
        return dbQueryResult.ToList();
    }
}