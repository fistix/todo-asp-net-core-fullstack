using Domain.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Commands
{
  public class AddTodoCommand : IRequest<TodoDto>
  {
    public AddTodoCommand(string title, string description, bool isDone)
    {
      Title = title;
      Description = description;
      IsDone = isDone;
    }

    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsDone { get; set; }
  }
}
