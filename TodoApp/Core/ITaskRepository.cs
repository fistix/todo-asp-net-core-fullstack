using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fistix.Training.Core
{
    public interface ITaskRepository
    {
        Task<Domain.DataModels.Task> Create(Domain.DataModels.Task task);

    }
}
