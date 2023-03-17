using MongoDB.Bson;

namespace ChatbotProject.Common.Infrastructure.Mongo;

public class BaseEntity
{
    public object Id { get; set; } = ObjectId.GenerateNewId();
    public DateTime Date { get; init; } = DateTime.Now;
}