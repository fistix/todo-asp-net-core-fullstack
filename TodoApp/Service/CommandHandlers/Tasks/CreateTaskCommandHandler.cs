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
  public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, CreateTaskCommandResult>
  {
    private readonly IMapper _mapper = null;
    private readonly ITaskRepository _taskRepository = null;
    public CreateTaskCommandHandler(IMapper mapper, ITaskRepository taskRepository)
    {
      _mapper = mapper;
      _taskRepository = taskRepository;
    }
    public async Task<CreateTaskCommandResult> Handle(CreateTaskCommand command, CancellationToken cancellationToken)
    {
      // build data-model Task from command using auto-mapper
      // use repository to save datamodel in db 
      // build dto from data-model using auto-mapper
      // build command result and return


      var task = _mapper.Map<Domain.DataModels.Task>(command);

      task.TaskId = Guid.NewGuid();
      task.CreatedOn = DateTime.Now;

      var response = await _taskRepository.Create(task);
      if (response)
      {
        return new CreateTaskCommandResult()
        {
          Payload = _mapper.Map<TaskDto>(task)
        };
      }
      else
      {
        return new CreateTaskCommandResult()
        {
          Payload = null
        };
      }
    }
  }
}
