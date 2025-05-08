using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IncommingPost.Domain.Interfaces;

public interface IRegistrationLogRepository : IRepository<RegistrationLog>
{
    Task<IEnumerable<RegistrationLog>> GetLogsByDocumentIdAsync(int documentId);
    Task<IEnumerable<RegistrationLog>> GetLogsByDateRangeAsync(DateTime startDate, DateTime endDate);
    Task AddLogEntryAsync(int documentId, string action);
}