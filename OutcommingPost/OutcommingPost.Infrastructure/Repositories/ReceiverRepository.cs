using OutcommingPost.Domain;
using OutcommingPost.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OutcommingPost.Infrastructure.Repositories
{
    public class ReceiverRepository : Repository<Receiver>, IReceiverRepository
    {
        public ReceiverRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Receiver>> GetActiveReceiversAsync()
        {
            // В реальном приложении здесь может быть свойство 'IsActive'
            // Для простоты возвращаем всех получателей
            return await GetAllAsync();
        }
    }
}