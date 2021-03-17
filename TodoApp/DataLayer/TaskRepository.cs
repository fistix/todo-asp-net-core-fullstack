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

    public async Task<bool> Create(Domain.DataModels.Task task)
    {
      await _efContext.Tasks.AddAsync(task);
      var dbChangesCount = await _efContext.SaveChangesAsync();
      return dbChangesCount > 0;
    }
   
    public async Task<bool> Update(Domain.DataModels.Task task)
    {
      _efContext.Tasks.Update(task);
      var dbChangesCount = await _efContext.SaveChangesAsync();
      return dbChangesCount > 0;
    }

    public async Task<bool> Delete(Guid id)
    {
      var task = await GetById(id);
      if (task != null)
      {
        _efContext.Tasks.Remove(task);
        var dbChangesCount = await _efContext.SaveChangesAsync();
        return dbChangesCount > 0;
      }
      throw new NotFoundException();
    }
    
    public async Task<List<Domain.DataModels.Task>> GetAll()
    {
      return await _efContext.Tasks.ToListAsync();
    }

    public async Task<Domain.DataModels.Task> GetById(Guid id)
    {
      var task = await _efContext.Tasks.FirstOrDefaultAsync(t => t.Id.Equals(id));
      if (task == null)
      {
        throw new NotFoundException("Task not found!");
      }
      return task;
    }

    public async Task<List<Domain.DataModels.Task>> GetTasksByProfileId(Guid id)
    {
      var tasks = await _efContext.Tasks.Where(t => t.UserId.Equals(id)).ToListAsync();
      if (tasks == null)
      {
        throw new NotFoundException("Tasks not found!");
      }
      return tasks;
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