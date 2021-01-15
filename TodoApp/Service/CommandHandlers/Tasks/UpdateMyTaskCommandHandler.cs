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
  public class UpdateMyTaskCommandhandler : IRequestHandler<UpdateMyTaskCommand, UpdateMyTaskCommandResult>
  {
    private readonly IMapper _mapper = null;
    private readonly ITaskRepository _taskRepository = null;
    private readonly IProfileRepository _profileRepository = null;
    private readonly ICurrentUserService _currentUserService = null;
    public UpdateMyTaskCommandhandler(IMapper mapper, ITaskRepository taskRepository,
      IProfileRepository profileRepository, ICurrentUserService currentUserService)
    {
      _mapper = mapper;
      _taskRepository = taskRepository;
      _profileRepository = profileRepository;
      _currentUserService = currentUserService;
    }
    public async Task<UpdateMyTaskCommandResult> Handle(UpdateMyTaskCommand command, CancellationToken cancellationToken)
    {
      var profile = await _profileRepository.GetByEmail(_currentUserService.Email);

      var task = await _taskRepository.GetById(command.Id);
      //
      if (task.UserProfileId.Equals(profile.ProfileId))
      {
        var updatedTask = _mapper.Map(command, task);

        updatedTask.ModifiedOn = DateTime.Now;

        var response = await _taskRepository.Update(updatedTask);
        if (response)
        {
          return new UpdateMyTaskCommandResult()
          {
            Payload = _mapper.Map<TaskDto>(updatedTask)
          };
        }
        else
        {
          return new UpdateMyTaskCommandResult()
          {
            Payload = _mapper.Map<TaskDto>(task)
          };
        }
      }
      else
      {
        throw new InvalidOperationException();
      }
    }
  }
}
