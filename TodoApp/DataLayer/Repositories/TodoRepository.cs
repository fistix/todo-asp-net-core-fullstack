using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Fistix.Training.Core;
using Fistix.Training.Domain.DataModels;

namespace Fistix.Training.DataLayer.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly EfContext _efContext = null;
        public TodoRepository(EfContext dbContext)
        {
            _efContext = dbContext;
        }

        public async Task<Todo> Add(Todo todo)
        {
            var entity = await _efContext.Todos.AddAsync(todo);
            return entity.Entity;
        }

        public async Task<Todo> Update(Todo todo)
        {
            var entity = _efContext.Todos.Update(todo);
            return entity.Entity;
        }

        public async Task<bool> Delete(Guid id)
        {
            var todo = await Get(id);
            if (todo != null)
            {
                _efContext.Todos.Remove(todo);
                return true;
            }
            throw new ArgumentException($"{id} not found Or invalid Id!");
        }

        public async Task<Todo> Get(Guid id)
        {
            return await _efContext.Todos.FirstOrDefaultAsync(t => t.Id.Equals(id));
        }

        public async Task<List<Todo>> Search(string term)
        {
            var result = (from c in _efContext.Todos
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
            var entity = await _efContext.Todos.ToListAsync();
            return entity;
        }

        public async Task<bool> SaveChanges()
        {
            var result = await _efContext.SaveChangesAsync();
            return result > 0;
        }

    }
}
