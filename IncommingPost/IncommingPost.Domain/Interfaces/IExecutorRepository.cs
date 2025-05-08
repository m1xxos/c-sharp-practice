using System.Collections.Generic;
using System.Threading.Tasks;

namespace IncommingPost.Domain.Interfaces;

public interface IExecutorRepository : IRepository<Executor>
{
    Task<Executor> GetExecutorWithAssignedDocumentsAsync(int id);
    Task<IEnumerable<Executor>> GetExecutorsByDepartmentAsync(string department);
    Task<IEnumerable<Executor>> SearchExecutorsByNameAsync(string nameFragment);
}