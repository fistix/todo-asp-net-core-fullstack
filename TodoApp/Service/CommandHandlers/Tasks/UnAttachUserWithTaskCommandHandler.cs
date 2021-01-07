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
        private readonly IProfileRepository _profileRepository = null;
        public UnAttachUserWithTaskCommandHandler(IMapper mapper, ITaskRepository taskRepository, IProfileRepository profileRepository)
        {
            _mapper = mapper;
            _taskRepository = taskRepository;
            _profileRepository = profileRepository;
        }
        public async Task<UnAttachUserWithTaskCommandResult> Handle(UnAttachUserWithTaskCommand command, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetById(command.TaskId);
            var assignedUser = await _taskRepository.CheckAssignedUser(command.UserId);
            task.UserProfileId = null;
            var response = await _taskRepository.Update(task);
            if (response != null)
            {
                return new UnAttachUserWithTaskCommandResult()
                {
                    Payload=_mapper.Map<Domain.Dtos.TaskDto>(response)
                };
            }
            return null;
            //throw new NotImplementedException();
        }
    }
}
