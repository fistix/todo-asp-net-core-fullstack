using AutoMapper;
using Fistix.Training.Core;
using Fistix.Training.Domain.Commands.MyProfile;
using Fistix.Training.Domain.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fistix.Training.Service.CommandHandlers.MyProfile
{
  
  public class CreateMyProfileCommandHandler : IRequestHandler<CreateMyProfileCommand, CreateMyProfileCommandResult>
  {
    private readonly IMapper _mapper = null;
    private readonly IProfileRepository _profileRepository = null;
    private readonly ICurrentUserService _currentUserService = null;
    public CreateMyProfileCommandHandler(IMapper mapper, IProfileRepository profileRepository, ICurrentUserService currentUserService)
    {
      _mapper = mapper;
      _profileRepository = profileRepository;
      _currentUserService = currentUserService;
    }
    public async Task<CreateMyProfileCommandResult> Handle(CreateMyProfileCommand command, CancellationToken cancellationToken)
    {
      var profile = _mapper.Map<Domain.DataModels.Profile>(command);

      profile.Email = _currentUserService.Email;

      profile.Id = Guid.NewGuid();
      var response = await _profileRepository.Create(profile);
      if (response)
      {
        return new CreateMyProfileCommandResult()
        {
          Payload = _mapper.Map<ProfileDto>(profile)
        };
      }
      else
      {
        return new CreateMyProfileCommandResult()
        {
          Payload = null
        };
      }
    }
  }
}
