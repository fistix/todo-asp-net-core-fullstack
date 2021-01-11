using Fistix.Training.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Fistix.Training.Core.Exceptions;

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
        public async Task<Domain.DataModels.Task> Update(Domain.DataModels.Task task)
        {
            var entity = _efContext.Tasks.Update(task);
            await _efContext.SaveChangesAsync();
            return entity.Entity;
        }

        //public async Task<Domain.DataModels.Task> Update(Domain.DataModels.Task task)
        //{
        //    var temp = _efContext.Tasks.FirstOrDefault(t => t.TaskId.Equals(task.TaskId));
        //    if (temp != null)
        //    {
        //        temp.Title = task.Title;
        //        temp.Description = task.Description;
        //        temp.Active = task.Active;
        //        temp.ModifiedOn = task.ModifiedOn;
        //        temp.UserProfileId = task.UserProfileId;

        //        var entity = _efContext.Tasks.Update(temp);
        //        await _efContext.SaveChangesAsync();
        //        return entity.Entity;
        //    }
        //    throw new NotFoundException("Result not found!");
        //    //_efContext.Tasks.Update(task).Property(t=>t.CreatedOn).IsModified==false;
        //}

        public async Task<bool> Delete(Guid id)
        {
            var task = await GetById(id);
            if (task != null)
            {
                _efContext.Remove(task);
                await _efContext.SaveChangesAsync();
                return true;
            }
            throw new NotFoundException();
        }


        public async Task<Domain.DataModels.Task> GetById(Guid id)
        {
            var task = await _efContext.Tasks.FirstOrDefaultAsync(t => t.TaskId.Equals(id));
            if (task == null)
            {
                throw new NotFoundException("Task not found!");
            }
            return task;
        }

        public async Task<List<Domain.DataModels.Task>> GetAll()
        {
            var entity = await _efContext.Tasks.ToListAsync();
            return entity;
        }

        //public async Task<Domain.DataModels.Task> CheckAssignedUser(Guid id)
        //{
        //    var user = await _efContext.Tasks.FirstOrDefaultAsync(x => x.UserProfileId.Equals(id));
        //    if (user == null)
        //    {
        //        throw new InvalidOperationException("User is not assigned to this task!");
        //    }
        //    return user;
        //}
    }
}
