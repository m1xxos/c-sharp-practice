using Microsoft.EntityFrameworkCore;
using OutcommingPost.Domain;
using OutcommingPost.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OutcommingPost.Infrastructure.Repositories
{
    public class InitiatorRepository : Repository<Initiator>, IInitiatorRepository
    {
        public InitiatorRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Initiator>> GetActiveInitiatorsAsync()
        {
            // В реальном приложении здесь может быть свойство 'IsActive'
            // Для простоты возвращаем всех инициаторов
            return await GetAllAsync();
        }
    }
}