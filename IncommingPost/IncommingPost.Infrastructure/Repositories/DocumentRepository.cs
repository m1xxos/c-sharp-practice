using IncommingPost.Domain;
using IncommingPost.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IncommingPost.Infrastructure.Repositories;

public class DocumentRepository : BaseRepository<Document>, IDocumentRepository
{
    public DocumentRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Document>> GetDocumentsByTypeAsync(int typeId)
    {
        return await _entities.Where(d => d.DocumentTypeId == typeId).ToListAsync();
    }

    public async Task<IEnumerable<Document>> GetDocumentsBySenderAsync(int senderId)
    {
        return await _entities.Where(d => d.SenderId == senderId).ToListAsync();
    }

    public async Task<IEnumerable<Document>> GetDocumentsForExecutorAsync(int executorId)
    {
        return await _entities
            .Include(d => d.AssignedTo)
            .Where(d => d.AssignedTo.Any(e => e.Id == executorId))
            .ToListAsync();
    }

    public async Task<IEnumerable<Document>> GetDocumentsByStatusAsync(string status)
    {
        return await _entities.Where(d => d.ProcessingStatus == status).ToListAsync();
    }

    public async Task AssignExecutorToDocumentAsync(int documentId, int executorId)
    {
        var document = await _entities
            .Include(d => d.AssignedTo)
            .FirstOrDefaultAsync(d => d.Id == documentId);
            
        var executor = await _context.Executors.FindAsync(executorId);
        
        if (document != null && executor != null)
        {
            document.AssignedTo.Add(executor);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<Document> GetDocumentWithDetailsAsync(int id)
    {
        return await _entities
            .Include(d => d.ReceivedFrom)
            .Include(d => d.ClassifiedAs)
            .Include(d => d.AssignedTo)
            .Include(d => d.RecordedIn)
            .FirstOrDefaultAsync(d => d.Id == id);
    }
}