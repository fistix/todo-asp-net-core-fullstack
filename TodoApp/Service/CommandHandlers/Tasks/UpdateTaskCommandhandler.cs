using AutoMapper;
using Fistix.Training.Core;
using Fistix.Training.Domain.Commands.Tasks;
using Fistix.Training.Domain.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fistix.Training.Service.CommandHandlers.Tasks
{
  public class UpdateTaskCommandhandler : IRequestHandler<UpdateTaskCommand, UpdateTaskCommandResult>
  {
    private readonly IMapper _mapper = null;
    private readonly ITaskRepository _taskRepository = null;
    public UpdateTaskCommandhandler(IMapper mapper, ITaskRepository taskRepository)
    {
      _mapper = mapper;
      _taskRepository = taskRepository;
    }
    public async Task<UpdateTaskCommandResult> Handle(UpdateTaskCommand command, CancellationToken cancellationToken)
    {
      var task = await _taskRepository.GetById(command.Id);
      var updatedTask = _mapper.Map(command, task);

      updatedTask.ModifiedOn = DateTime.Now;

      var response = await _taskRepository.Update(updatedTask);
      if (response)
      {
        return new UpdateTaskCommandResult()
        {
          Payload = _mapper.Map<TaskDto>(updatedTask)
        };
      }
      else
      {
        return new UpdateTaskCommandResult()
        {
          Payload = _mapper.Map<TaskDto>(task)
        };
      }
    }
  }
}
