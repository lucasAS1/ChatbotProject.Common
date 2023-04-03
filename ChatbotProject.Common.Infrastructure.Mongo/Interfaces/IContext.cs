using MongoDB.Driver;

namespace ChatbotProject.Common.Infrastructure.Mongo.Interfaces;

public interface IContext
{
    public IMongoCollection<TDocument> GetCollection<TDocument>(string entityName);
}