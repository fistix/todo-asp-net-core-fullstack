using Fistix.Training.Domain.Queries.Tasks;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fistix.Training.Domain.Dtos;
using Fistix.Training.Core;
using AutoMapper;
using System.Threading;

namespace Fistix.Training.Service.QueryHandlers.Tasks
{
    public class GetAllTasksQueryHandler : IRequestHandler<GetAllTasksQuery, GetAllTasksQueryResult>
    {
        private readonly ITaskRepository _taskRepository = null;
        private readonly IMapper _mapper = null;
        public GetAllTasksQueryHandler(ITaskRepository taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        public async Task<GetAllTasksQueryResult> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
        {
            //return _mapper.Map<GetAllTasksQueryResult>(await _taskRepository.GetAll());
            var tasks = _mapper.Map<List<TaskDto>>(await _taskRepository.GetAll());
            return new GetAllTasksQueryResult()
            {
                //Payload = _mapper.Map<Domain.Dtos.TaskDto>(tasks)
                Payload = tasks
            };

            //return _mapper.Map<GetAllTasksQueryResult>(await _taskRepository.GetAll());
        }
    }
}
