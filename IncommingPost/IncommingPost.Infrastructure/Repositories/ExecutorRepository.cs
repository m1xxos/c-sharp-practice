using IncommingPost.Domain;
using IncommingPost.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IncommingPost.Infrastructure.Repositories;

public class ExecutorRepository : BaseRepository<Executor>, IExecutorRepository
{
    public ExecutorRepository(ApplicationDbContext context) : base(context)
    {
    }
    
    public async Task<Executor> GetExecutorWithAssignedDocumentsAsync(int id)
    {
        return await _entities
            .Include(e => e.AssignedDocuments)
            .FirstOrDefaultAsync(e => e.Id == id);
    }
    
    public async Task<IEnumerable<Executor>> GetExecutorsByDepartmentAsync(string department)
    {
        return await _entities
            .Where(e => e.Department == department)
            .ToListAsync();
    }
    
    public async Task<IEnumerable<Executor>> SearchExecutorsByNameAsync(string nameFragment)
    {
        return await _entities
            .Where(e => e.FullName.Contains(nameFragment))
            .ToListAsync();
    }
}