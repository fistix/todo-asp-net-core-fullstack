using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
  public interface ITodoRepository
  {
    Task<Todo> Add(Todo todo);
    Task<Todo> Update(Todo todo);
    Task<bool> Delete(Guid id);
    Task<Todo> Get(Guid id);
    Task<List<Todo>> Search(string term);
    Task<List<Todo>> Get();
    Task<bool> SaveChanges();

  }
}
