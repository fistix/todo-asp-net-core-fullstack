using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fistix.Training.Core
{
    public interface ITaskRepository
    {
        Task<bool> Create(Domain.DataModels.Task task);
        Task<bool> Update(Domain.DataModels.Task task);
        Task<bool> Delete(Guid id);
        Task<Domain.DataModels.Task> GetById(Guid id);
        Task<List<Domain.DataModels.Task>> GetAll();
        //Task<Domain.DataModels.Task> CheckAssignedUser(Guid id);
    }
}
