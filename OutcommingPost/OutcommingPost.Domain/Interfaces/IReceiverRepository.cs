using System.Collections.Generic;
using System.Threading.Tasks;

namespace OutcommingPost.Domain.Interfaces
{
    public interface IReceiverRepository : IRepository<Receiver>
    {
        Task<IEnumerable<Receiver>> GetActiveReceiversAsync();
    }
}