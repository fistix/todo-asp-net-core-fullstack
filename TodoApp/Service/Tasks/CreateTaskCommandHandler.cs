using Domain.Commands.Tasks;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Service.Tasks
{
  public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, CreateTaskCommandResult>
  {
    public Task<CreateTaskCommandResult> Handle(CreateTaskCommand command, CancellationToken cancellationToken)
    {      
      // build data-model Task from command using auto-mapper
      // use repository to save datamodel in db 
      // build dto from data-model using auto-mapper
      // build command result and return

      throw new NotImplementedException();
    }
  }
}
