using AutoMapper;
using Fistix.Training.Core;
using Fistix.Training.Domain.Commands.Tasks;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fistix.Training.Service.CommandHandlers.Tasks
{
  public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, DeleteTaskCommandResult>
  {
    private readonly ITaskRepository _taskRepository = null;
    public DeleteTaskCommandHandler(ITaskRepository taskRepository)
    {
      _taskRepository = taskRepository;
    }
    public async Task<DeleteTaskCommandResult> Handle(DeleteTaskCommand command, CancellationToken cancellationToken)
    {
      var response = await _taskRepository.Delete(command.Id);

      return new DeleteTaskCommandResult()
      {
        IsSucceed = response
      };
    }
  }
}
