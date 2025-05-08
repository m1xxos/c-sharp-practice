using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace OutcommingPost.Domain.Interfaces
{
    public interface IDocumentRepository : IRepository<Document>
    {
        Task<IEnumerable<Document>> GetDocumentsByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<Document> AssignRegistrationNumberAsync(int documentId);
    }
}