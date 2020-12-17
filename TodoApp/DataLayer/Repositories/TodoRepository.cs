using Core;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace DataLayer.Repositories
{
  public class TodoRepository : ITodoRepository
  {
    private readonly TodoDbContext _dbContext = null;
    public TodoRepository(TodoDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public async Task<Todo> Add(Todo todo)
    {
      var entity = await _dbContext.Todos.AddAsync(todo);
      return entity.Entity;
    }

    public async Task<Todo> Update(Todo todo)
    {
      var entity = _dbContext.Todos.Update(todo);
      return entity.Entity;
    }

    public async Task<bool> Delete(Guid id)
    {
      var todo = await Get(id);
      if (todo != null)
      {
        _dbContext.Todos.Remove(todo);
        return true;
      }
      throw new ArgumentException($"{id} not found Or invalid Id!");
    }

    public async Task<Todo> Get(Guid id)
    {
      return await _dbContext.Todos.FirstOrDefaultAsync(t => t.Id.Equals(id));
    }

    public async Task<List<Todo>> Search(string term)
    {
      var result = (from c in _dbContext.Todos
                    where c.Title.Contains(term) || c.Description.Contains(term)
                    select new Todo()
                    {
                      Id = c.Id,
                      Title = c.Title,
                      Description = c.Description,
                      CreatedOn = c.CreatedOn,
                      ModifiedOn = c.ModifiedOn
                    }).ToList();
      return result;
    }

    public async Task<List<Todo>> Get()
    {
      var entity = await _dbContext.Todos.ToListAsync();
      return entity;
    }

    public async Task<bool> SaveChanges()
    {
      var result = await _dbContext.SaveChangesAsync();
      return result > 0;
    }

  }
}
