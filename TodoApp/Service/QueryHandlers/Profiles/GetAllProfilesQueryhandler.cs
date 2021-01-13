using AutoMapper;
using Fistix.Training.Core;
using Fistix.Training.Domain.Dtos;
using Fistix.Training.Domain.Queries.Profiles;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fistix.Training.Service.QueryHandlers.Profiles
{
  public class GetAllProfilesQueryhandler : IRequestHandler<GetAllProfilesQuery, GetAllProfilesQueryResult>
  {
    private readonly IMapper _mapper = null;
    private readonly IProfileRepository _profileRepository = null;
    public GetAllProfilesQueryhandler(IMapper mapper, IProfileRepository profileRepository)
    {
      _mapper = mapper;
      _profileRepository = profileRepository;
    }

    public async Task<GetAllProfilesQueryResult> Handle(GetAllProfilesQuery request, CancellationToken cancellationToken)
    {
      var profiles = _mapper.Map<List<ProfileDto>>(await _profileRepository.GetAll());
      return new GetAllProfilesQueryResult()
      {
        Payload = profiles
      };
    }
  }
}