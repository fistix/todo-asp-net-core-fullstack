using Fistix.Training.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace Fistix.Training.DataLayer.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly EfContext _efContext = null;
        public TaskRepository(EfContext efContext)
        {
            _efContext = efContext;
        }

        public async Task<Domain.DataModels.Task> Create(Domain.DataModels.Task task)
        {
            var entity = await _efContext.Tasks.AddAsync(task);
            await _efContext.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<List<Domain.DataModels.Task>> GetAll()
        {
            var entity = await _efContext.Tasks.ToListAsync();
            return entity;
        }

        public async Task<Domain.DataModels.Task> GetById(Guid id)
        {
            return await _efContext.Tasks.FirstOrDefaultAsync(t => t.Id.Equals(id));
        }

        public async Task<Domain.DataModels.Task> Update(Domain.DataModels.Task task)
        {

            //var temp = await GetById(task.Id.GetValueOrDefault());
            //if (temp != null)
            //{
            //    task.CreatedOn = temp.CreatedOn;
            //}

            //var temp = await GetById((Guid)task.Id);

            var temp = _efContext.Tasks.FirstOrDefault(t => t.Id.Equals(task.Id));
            if (temp == null)
            {
                return null;
            }

            var entity = _efContext.Tasks.Update(task);
            await _efContext.SaveChangesAsync();
            return entity.Entity;

            //_efContext.Tasks.Update(task).Property(t=>t.CreatedOn).IsModified==false;
        }


    }
}
