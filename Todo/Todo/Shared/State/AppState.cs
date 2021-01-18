using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Shared.Models;

namespace Todo.Shared.State
{
  public class AppState
  {
    public List<TaskDetail> Tasks { get; private set; }

    public event Action OnChange;

    public void OnInitSetTodos(List<TaskDetail> taskDetails)
    {
      Tasks = taskDetails;
    }

    public void SetTodoList(List<TaskDetail> taskDetails)
    {
      Tasks = taskDetails;
      NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
  }
}
