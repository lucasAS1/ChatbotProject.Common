using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Text.Json;
using ChatbotProject.Common.Domain.Models.Settings;
using ChatbotProject.Common.Infrastructure.Mongo.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace ChatbotProject.Common.Infrastructure.Mongo;

[ExcludeFromCodeCoverage]
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
        _collection = mongoDatabase.GetCollection<TDocument>(typeof(TDocument).ToString());
    }
    
    public async Task AddOrUpdateDocument(TDocument document)
    {
        var updatedDocument = await UpdateIfExists(document);

        if (updatedDocument is null)
        {
            await _collection.InsertOneAsync(document);
        }
    }

    private async Task<TDocument> UpdateIfExists(TDocument document)
    {
        var filter = new ExpressionFilterDefinition<TDocument>(x => x.Id == document.Id);

        var updatedDocument = await _collection.FindOneAndReplaceAsync(filter, document);
        return updatedDocument;
    }

    public async Task DeleteDocument(TDocument document)
    {
        var expressionFilterDefinition = new ExpressionFilterDefinition<TDocument>(x => x.Id == document.Id);
        
        await _collection.DeleteOneAsync(expressionFilterDefinition);
    }

    public async Task<TDocument> GetDocument(Expression<Func<TDocument,bool>> filter)
    {
        var expressionFilterDefinition = new ExpressionFilterDefinition<TDocument>(filter);
        var dbQueryResult = await _collection.FindAsync(expressionFilterDefinition);
        
        return dbQueryResult.FirstOrDefault();
    }

    public async Task<List<TDocument>> GetDocuments(Expression<Func<TDocument,bool>> filter)
    {
        var expressionFilterDefinition = new ExpressionFilterDefinition<TDocument>(filter);
        
        var dbQueryResult = await _collection.FindAsync(expressionFilterDefinition);
        
        return dbQueryResult.ToList();
    }
}