using System.Collections.Generic;
using System.Threading.Tasks;

namespace IncommingPost.Domain.Interfaces;

public interface ISenderRepository : IRepository<Sender>
{
    Task<Sender> GetSenderWithDocumentsAsync(int id);
    Task<IEnumerable<Sender>> SearchSendersByNameAsync(string nameFragment);
}