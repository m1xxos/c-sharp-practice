using Microsoft.EntityFrameworkCore;
using OutcommingPost.Domain;
using OutcommingPost.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OutcommingPost.Infrastructure.Repositories
{
    public class DocumentRepository : Repository<Document>, IDocumentRepository
    {
        public DocumentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Document> AssignRegistrationNumberAsync(int documentId)
        {
            var document = await GetByIdAsync(documentId);
            if (document != null)
            {
                document.AssignNumber();
                await UpdateAsync(document);
                await SaveChangesAsync();
            }
            return document;
        }

        public async Task<IEnumerable<Document>> GetDocumentsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _dbSet
                .Where(d => d.CreationDate >= startDate && d.CreationDate <= endDate)
                .ToListAsync();
        }
    }
}