using IncommingPost.Domain;
using IncommingPost.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IncommingPost.Infrastructure.Repositories;

public class RegistrationLogRepository : BaseRepository<RegistrationLog>, IRegistrationLogRepository
{
    public RegistrationLogRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<RegistrationLog>> GetLogsByDocumentIdAsync(int documentId)
    {
        return await _entities
            .Where(l => l.DocumentId == documentId)
            .OrderByDescending(l => l.LogDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<RegistrationLog>> GetLogsByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await _entities
            .Where(l => l.LogDate >= startDate && l.LogDate <= endDate)
            .OrderByDescending(l => l.LogDate)
            .ToListAsync();
    }
    
    public async Task AddLogEntryAsync(int documentId, string action)
    {
        var logEntry = new RegistrationLog
        {
            DocumentId = documentId,
            Action = action,
            LogDate = DateTime.Now
        };
        
        await AddAsync(logEntry);
        await SaveChangesAsync();
    }
}