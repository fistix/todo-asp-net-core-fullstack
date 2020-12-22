using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fistix.Training.Domain.Commands
{
  public class DeleteTodoCommand : IRequest<bool>
  {
    public DeleteTodoCommand(string id)
    {
      Id = id;
    }
    public string Id { get; set; }

  }
}
