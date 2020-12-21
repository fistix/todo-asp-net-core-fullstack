using Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commands.Tasks
{
  public class CreateTaskCommandResult
  {
    public TodoDto Payload { get; set; }
  }
}
