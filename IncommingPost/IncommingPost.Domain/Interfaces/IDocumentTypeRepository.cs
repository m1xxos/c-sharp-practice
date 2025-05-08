using System.Collections.Generic;
using System.Threading.Tasks;

namespace IncommingPost.Domain.Interfaces;

public interface IDocumentTypeRepository : IRepository<DocumentType>
{
    Task<DocumentType> GetTypeWithDocumentsAsync(int id);
    Task<IEnumerable<DocumentType>> SearchTypesByNameAsync(string nameFragment);
}