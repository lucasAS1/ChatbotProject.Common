namespace ChatbotProject.Common.Infrastructure.Mongo.Interfaces;

public interface IRepository<TDocument>
    where TDocument : BaseEntity
{
    Task AddDocument(TDocument document);
    Task UpdateDocument(TDocument document);
    Task DeleteDocument(TDocument document);
    Task<TDocument> GetDocument(TDocument document);
    Task<List<TDocument>> GetDocuments(TDocument document);
}