using MongoDB.Bson;
using System.Diagnostics.CodeAnalysis;

namespace ChatbotProject.Common.Infrastructure.Mongo;

[ExcludeFromCodeCoverage]
public class BaseEntity
{
    public object Id { get; set; } = ObjectId.GenerateNewId();
    public DateTime Date { get; init; } = DateTime.Now;
}
