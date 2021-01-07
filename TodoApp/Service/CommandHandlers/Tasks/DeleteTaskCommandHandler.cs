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
        private readonly IMapper _mapper = null;
        private readonly ITaskRepository _taskRepository = null;
        public DeleteTaskCommandHandler(IMapper mapper, ITaskRepository taskRepository)
        {
            _mapper = mapper;
            _taskRepository = taskRepository;
        }
        public async Task<DeleteTaskCommandResult> Handle(DeleteTaskCommand command, CancellationToken cancellationToken)
        {
            //var task = _mapper.Map<Domain.DataModels.Task>(command);
            var result = await _taskRepository.Delete(command.Id);
            return new DeleteTaskCommandResult()
            {
                IsSucceed = result/*_mapper.Map<Domain.Dtos.TaskDto>(result)*/
            };
            //throw new NotImplementedException();
        }
    }
}
