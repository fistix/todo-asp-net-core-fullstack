using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commands.Tasks
{
  public class CreateTaskCommand: IRequest<CreateTaskCommandResult>
  { 
    public string Title { get; set; }
    public string Description { get; set; }
    public bool Active { get; set; }
    public Guid? UserId { get; set; }
  }
}
