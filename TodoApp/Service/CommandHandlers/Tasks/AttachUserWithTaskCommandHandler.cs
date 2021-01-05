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
        public AttachUserWithTaskCommandHandler(IMapper mapper,ITaskRepository taskRepository,IProfileRepository profileRepository)
        {
            _mapper = mapper;
            _taskRepository = taskRepository;
            _profileRepository = profileRepository;
        }
        public async Task<AttachUserWithTaskCommandResult> Handle(AttachUserWithTaskCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var tempTask = await _taskRepository.GetById(command.TaskId);
                //if (task == null)
                //{
                //    throw new NotFoundException();
                //}

                var tempProfile = await _profileRepository.GetById(command.UserId);
                //if (profile == null)
                //{
                //    throw new NotFoundException();
                //}
                if (tempProfile == null || tempTask == null)
                {
                    throw new NotFoundException();
                }
                var task = _mapper.Map<Domain.DataModels.Task>(command);
                var result = await _taskRepository.Update(task);



            }
            catch (Exception)
            {

                throw;
            }
            
            throw new NotImplementedException();
        }
    }
}
