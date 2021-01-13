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
    private readonly IMapper _mapper = null;
    private readonly ITaskRepository _taskRepository = null;
    public GetAllTasksQueryHandler(IMapper mapper, ITaskRepository taskRepository)
    {
      _mapper = mapper;
      _taskRepository = taskRepository;
    }

    public async Task<GetAllTasksQueryResult> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
    {
      var tasks = _mapper.Map<List<TaskDto>>(await _taskRepository.GetAll());
      return new GetAllTasksQueryResult()
      {
        Payload = tasks
      };
    }
  }
}