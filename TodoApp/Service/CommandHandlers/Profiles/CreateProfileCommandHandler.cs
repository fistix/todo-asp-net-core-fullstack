using AutoMapper;
using Fistix.Training.Core;
using Fistix.Training.Domain.Commands.Profiles;
using Fistix.Training.Domain.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fistix.Training.Service.CommandHandlers.Profiles
{
  public class CreateProfileCommandHandler : IRequestHandler<CreateProfileCommand, CreateProfileCommandResult>
  {
    private readonly IMapper _mapper = null;
    private readonly IProfileRepository _profileRepository = null;
    public CreateProfileCommandHandler(IMapper mapper, IProfileRepository profileRepository)
    {
      _mapper = mapper;
      _profileRepository = profileRepository;
    }
    public async Task<CreateProfileCommandResult> Handle(CreateProfileCommand command, CancellationToken cancellationToken)
    {
      var profile = _mapper.Map<Domain.DataModels.Profile>(command);
      profile.ProfileId = Guid.NewGuid();
      var response = await _profileRepository.Create(profile);
      if(response)
      {
        return new CreateProfileCommandResult()
        {
          Payload = _mapper.Map<ProfileDto>(profile)
        };
      }
      else
      {
        return new CreateProfileCommandResult()
        {
          Payload = null
        };
      }

    }
  }
}
