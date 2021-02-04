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
  public class GetMyAllTasksQueryhandler : IRequestHandler<GetMyAllTasksQuery, GetMyAllTasksQueryResult>
  {
    private readonly IMapper _mapper = null;
    private readonly ITaskRepository _taskRepository = null;
    private readonly IProfileRepository _profileRepository = null;
    private readonly ICurrentUserService _currentUserService = null;
    public GetMyAllTasksQueryhandler(IMapper mapper, ITaskRepository taskRepository,
      IProfileRepository profileRepository, ICurrentUserService currentUserService)
    {
      _mapper = mapper;
      _taskRepository = taskRepository;
      _profileRepository = profileRepository;
      _currentUserService = currentUserService;
    }

    public async Task<GetMyAllTasksQueryResult> Handle(GetMyAllTasksQuery request, CancellationToken cancellationToken)
    {
      var profile = await _profileRepository.GetByEmail(_currentUserService.Email);
      var tasks = _mapper.Map<List<TaskDto>>(await _taskRepository.GetTasksByProfileId(profile.Id));

      //var result = await _taskRepository.GetAllById(Guid.Parse(profile.ProfileId.ToString()));
      return new GetMyAllTasksQueryResult()
      {
        Payload = tasks
      };

      //throw new NotImplementedException();
    }
  }
}