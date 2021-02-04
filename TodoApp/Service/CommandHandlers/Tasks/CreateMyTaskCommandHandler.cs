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

  public class CreateMyTaskCommandHandler : IRequestHandler<CreateMyTaskCommand, CreateMyTaskCommandResult>
  {
    private readonly IMapper _mapper = null;
    private readonly ITaskRepository _taskRepository = null;
    private readonly IProfileRepository _profileRepository = null;
    private readonly ICurrentUserService _currentUserService = null;
    public CreateMyTaskCommandHandler(IMapper mapper, ITaskRepository taskRepository,
      IProfileRepository profileRepository, ICurrentUserService currentUserService)
    {
      _mapper = mapper;
      _taskRepository = taskRepository;
      _profileRepository = profileRepository;
      _currentUserService = currentUserService;
    }
    public async Task<CreateMyTaskCommandResult> Handle(CreateMyTaskCommand command, CancellationToken cancellationToken)
    {
      var profile = await _profileRepository.GetByEmail(_currentUserService.Email);
      
      var task = _mapper.Map<Domain.DataModels.Task>(command);

      task.UserId = profile.Id;
      task.Id = Guid.NewGuid();
      task.CreatedOn = DateTime.Now;

      var response = await _taskRepository.Create(task);
      if (response)
      {
        return new CreateMyTaskCommandResult()
        {
          Payload = _mapper.Map<TaskDto>(task)
        };
      }
      else
      {
        return new CreateMyTaskCommandResult()
        {
          Payload = null
        };
      }
    }
  }
}
