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
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, CreateTaskCommandResult>
    {

        private readonly ITaskRepository _taskRepository = null;
        private readonly IMapper _mapper = null;
        public CreateTaskCommandHandler(IMapper mapper, ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }
        public async Task<CreateTaskCommandResult> Handle(CreateTaskCommand command, CancellationToken cancellationToken)
        {
            // build data-model Task from command using auto-mapper
            // use repository to save datamodel in db 
            // build dto from data-model using auto-mapper
            // build command result and return


            var task = _mapper.Map<Domain.DataModels.Task>(command);
            
            task.Id = Guid.NewGuid();
            task.CreatedOn = DateTime.Now;
            
            var entity = await _taskRepository.Create(task);
            return new CreateTaskCommandResult()
            {
                Payload = _mapper.Map<Domain.Dtos.TaskDto>(entity)
            };

            //throw new NotImplementedException();
        }
    }
}
