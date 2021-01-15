using AutoMapper;
using Fistix.Training.Core;
using Fistix.Training.Domain.Dtos;
using Fistix.Training.Domain.Queries.MyProfile;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fistix.Training.Service.QueryHandlers.MyProfile
{
  public class GetMyProfileDetailByEmailQueryHandler : IRequestHandler<GetMyProfileDetailByEmailQuery, GetMyProfileDetailByEmailQueryResult>
  {
    private readonly IMapper _mapper = null;
    private readonly IProfileRepository _profileRepository = null;
    public GetMyProfileDetailByEmailQueryHandler(IMapper mapper, IProfileRepository profileRepository)
    {
      _mapper = mapper;
      _profileRepository = profileRepository;
    }
    public async Task<GetMyProfileDetailByEmailQueryResult> Handle(GetMyProfileDetailByEmailQuery request, CancellationToken cancellationToken)
    {
      var result = _mapper.Map<ProfileDto>(await _profileRepository.GetByEmail(request.Email));
      return new GetMyProfileDetailByEmailQueryResult()
      {
        Payload = result
      };
    }
  }
}
