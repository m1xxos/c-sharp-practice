using System.Collections.Generic;
using System.Threading.Tasks;

namespace IncommingPost.Domain.Interfaces;

public interface IDocumentRepository : IRepository<Document>
{
    Task<IEnumerable<Document>> GetDocumentsByTypeAsync(int typeId);
    Task<IEnumerable<Document>> GetDocumentsBySenderAsync(int senderId);
    Task<IEnumerable<Document>> GetDocumentsForExecutorAsync(int executorId);
    Task<IEnumerable<Document>> GetDocumentsByStatusAsync(string status);
    Task AssignExecutorToDocumentAsync(int documentId, int executorId);
    Task<Document> GetDocumentWithDetailsAsync(int id);
}