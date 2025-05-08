using IncommingPost.Domain;
using IncommingPost.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IncommingPost.Infrastructure.Repositories;

public class DocumentTypeRepository : BaseRepository<DocumentType>, IDocumentTypeRepository
{
    public DocumentTypeRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<DocumentType> GetTypeWithDocumentsAsync(int id)
    {
        return await _entities
            .Include(t => t.Documents)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<IEnumerable<DocumentType>> SearchTypesByNameAsync(string nameFragment)
    {
        return await _entities
            .Where(t => t.TypeName.Contains(nameFragment))
            .ToListAsync();
    }
}