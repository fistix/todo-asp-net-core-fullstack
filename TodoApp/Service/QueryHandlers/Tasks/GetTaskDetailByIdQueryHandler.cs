using AutoMapper;
using Fistix.Training.Core;
using Fistix.Training.Domain.Dtos;
using Fistix.Training.Domain.Queries.Tasks;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fistix.Training.Service.QueryHandlers.Tasks
{
    public class GetTaskDetailByIdQueryHandler : IRequestHandler<GetTaskDetailByIdQuery, GetTaskDetailByIdQueryResult>
    {
        private readonly IMapper _mapper = null;
        private readonly ITaskRepository _taskRepository = null;
        public GetTaskDetailByIdQueryHandler(ITaskRepository taskRepository, IMapper mapper)
        {
            _mapper = mapper;
            _taskRepository = taskRepository;
        }
        public async Task<GetTaskDetailByIdQueryResult> Handle(GetTaskDetailByIdQuery request, CancellationToken cancellationToken)
        {
            var result = _mapper.Map<TaskDto>(await _taskRepository.GetById(request.Id));
            return new GetTaskDetailByIdQueryResult()
            {
                Payload = result
            };
        }
    }
}
