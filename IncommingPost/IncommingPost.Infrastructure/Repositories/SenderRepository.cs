using IncommingPost.Domain;
using IncommingPost.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IncommingPost.Infrastructure.Repositories;

public class SenderRepository : BaseRepository<Sender>, ISenderRepository
{
    public SenderRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Sender> GetSenderWithDocumentsAsync(int id)
    {
        return await _entities
            .Include(s => s.Documents)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<IEnumerable<Sender>> SearchSendersByNameAsync(string nameFragment)
    {
        return await _entities
            .Where(s => s.Name.Contains(nameFragment))
            .ToListAsync();
    }
}