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
  public class GetMyTaskDetailQueryHandler : IRequestHandler<GetMyTaskDetailQuery, GetMyTaskDetailQueryResult>
  {
    private readonly IMapper _mapper = null;
    private readonly ITaskRepository _taskRepository = null;
    private readonly IProfileRepository _profileRepository = null;
    private readonly ICurrentUserService _currentUserService = null;
    public GetMyTaskDetailQueryHandler(ITaskRepository taskRepository, IMapper mapper,
      IProfileRepository profileRepository, ICurrentUserService currentUserService)
    {
      _mapper = mapper;
      _taskRepository = taskRepository;
      _profileRepository = profileRepository;
      _currentUserService = currentUserService;
    }

    public async Task<GetMyTaskDetailQueryResult> Handle(GetMyTaskDetailQuery request, CancellationToken cancellationToken)
    {
      

      var result = _mapper.Map<TaskDto>(await _taskRepository.GetById(request.Id));

      var profile = await _profileRepository.GetByEmail(_currentUserService.Email);
      if (result.UserProfileId.Equals(profile.ProfileId))
      {

        return new GetMyTaskDetailQueryResult()
        {
          Payload = result
        };
      }
      else
      {
        //return new GetMyTaskDetailQueryResult()
        //{
        //  Payload = null
        //};
        throw new InvalidOperationException("Task with this Profile does not exists!");
      }
    }
  }
}
