using Fistix.Training.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fistix.Training.DataLayer.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TodoDbContext _dbContext = null;
        public TaskRepository(TodoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Domain.DataModels.Task> Create(Domain.DataModels.Task task)
        {
            var entity = await _dbContext.Tasks.AddAsync(task);
            await _dbContext.SaveChangesAsync();
            return entity.Entity;
            //return null;
            
        }

    }
}
