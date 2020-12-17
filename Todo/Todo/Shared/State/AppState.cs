using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Shared.Models;

namespace Todo.Shared.State
{
  public class AppState
  {
    public List<TodoDetail> Todos { get; private set; }

    public event Action OnChange;

    public void OnInitSetTodos(List<TodoDetail> todoDetails)
    {
      Todos = todoDetails;
    }

    public void SetTodoList(List<TodoDetail> todoDetails)
    {
      Todos = todoDetails;
      NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
  }
}
