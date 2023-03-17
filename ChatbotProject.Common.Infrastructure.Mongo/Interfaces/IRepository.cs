using System.Linq.Expressions;

namespace ChatbotProject.Common.Infrastructure.Mongo.Interfaces;

public interface IRepository<TDocument>
    where TDocument : BaseEntity
{
    Task AddOrUpdateDocument(TDocument document);
    Task DeleteDocument(TDocument document);
    Task<TDocument> GetDocument(Expression<Func<TDocument,bool>> filter);
    Task<List<TDocument>> GetDocuments(Expression<Func<TDocument,bool>> filter);
}