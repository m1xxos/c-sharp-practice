using System.Collections.Generic;
using System.Threading.Tasks;

namespace OutcommingPost.Domain.Interfaces
{
    public interface IInitiatorRepository : IRepository<Initiator>
    {
        Task<IEnumerable<Initiator>> GetActiveInitiatorsAsync();
    }
}