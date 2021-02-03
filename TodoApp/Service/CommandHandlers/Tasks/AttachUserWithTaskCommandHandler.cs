using AutoMapper;
using Fistix.Training.Core;
using Fistix.Training.Core.Exceptions;
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
  public class AttachUserWithTaskCommandHandler : IRequestHandler<AttachUserWithTaskCommand, AttachUserWithTaskCommandResult>
  {
    private readonly IMapper _mapper = null;
    private readonly ITaskRepository _taskRepository = null;
    private readonly IProfileRepository _profileRepository = null;
    public AttachUserWithTaskCommandHandler(IMapper mapper, ITaskRepository taskRepository, IProfileRepository profileRepository)
    {
      _mapper = mapper;
      _taskRepository = taskRepository;
      _profileRepository = profileRepository;
    }
    public async Task<AttachUserWithTaskCommandResult> Handle(AttachUserWithTaskCommand command, CancellationToken cancellationToken)
    {
      var task = await _taskRepository.GetById(command.TaskId);
      var profile = await _profileRepository.GetById(command.UserId);

      task.UserProfileId = profile.ProfileId;
      var response = await _taskRepository.Update(task);
      if (response)
      {
        return new AttachUserWithTaskCommandResult()
        {
          Payload = _mapper.Map<Domain.Dtos.TaskDto>(task)
        };
      }
      else
      {
        return new AttachUserWithTaskCommandResult()
        {
          Payload = null
        };
      }
    }
  }
}
