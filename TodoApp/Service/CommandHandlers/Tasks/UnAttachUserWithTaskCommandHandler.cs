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
  public class UnAttachUserWithTaskCommandHandler : IRequestHandler<UnAttachUserWithTaskCommand, UnAttachUserWithTaskCommandResult>
  {
    private readonly IMapper _mapper = null;
    private readonly ITaskRepository _taskRepository = null;
    public UnAttachUserWithTaskCommandHandler(IMapper mapper, ITaskRepository taskRepository)
    {
      _mapper = mapper;
      _taskRepository = taskRepository;
    }
    public async Task<UnAttachUserWithTaskCommandResult> Handle(UnAttachUserWithTaskCommand command, CancellationToken cancellationToken)
    {
      var task = await _taskRepository.GetById(command.TaskId);
      //var assignedUser = await _taskRepository.CheckAssignedUser(command.UserId);

      if (task.UserId != command.UserId)
      {
        throw new InvalidOperationException("User is not assigned to this task!");
      }

      task.UserId = null;
      var response = await _taskRepository.Update(task);
      if (response)
      {
        return new UnAttachUserWithTaskCommandResult()
        {
          Payload = _mapper.Map<Domain.Dtos.TaskDto>(response)
        };
      }
      else
      {
        return new UnAttachUserWithTaskCommandResult()
        {
          Payload = null
        };
      }
    }
  }
}
