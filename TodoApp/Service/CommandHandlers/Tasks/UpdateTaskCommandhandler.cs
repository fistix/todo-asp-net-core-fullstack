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
            //var task = _mapper.Map<Domain.DataModels.Task>(command);
            task = _mapper.Map(command, task);
            task.ModifiedOn = DateTime.Now;

            var result = await _taskRepository.Update(task);
            if (result != null)
            {
                return new UpdateTaskCommandResult()
                {
                    Payload = _mapper.Map<Domain.Dtos.TaskDto>(result)
                };
            }
            return null;
            //throw new NotImplementedException();
        }
    }
}
