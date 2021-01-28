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

namespace Fistix.Training.Service.CommandHandlers.MyProfile
{
  public class UpdateMyProfileCommandHandler : IRequestHandler<UpdateMyProfileCommand, UpdateMyProfileCommandResult>
  {
    private readonly IMapper _mapper = null;
    private readonly IProfileRepository _profileRepository = null;
    private readonly ICurrentUserService _currentUserService = null;
    public UpdateMyProfileCommandHandler(IMapper mapper, IProfileRepository profileRepository,
      ICurrentUserService currentUserService)
    {
      _mapper = mapper;
      _profileRepository = profileRepository;
      _currentUserService = currentUserService;
    }
    public async Task<UpdateMyProfileCommandResult> Handle(UpdateMyProfileCommand command, CancellationToken cancellationToken)
    {
      //command.Email = _currentUserService.Email;
      //var profile = await _profileRepository.GetByEmail(command.Email);

      var profile = await _profileRepository.GetByEmail(_currentUserService.Email);
      var updatedProfile = _mapper.Map(command, profile);
      var response = await _profileRepository.Update(updatedProfile);
      if (response)
      {
        return new UpdateMyProfileCommandResult()
        {
          Payload = _mapper.Map<ProfileDto>(updatedProfile)
        };
      }
      else
      {
        return new UpdateMyProfileCommandResult()
        {
          Payload = _mapper.Map<ProfileDto>(profile)
        };
      }
    }
  }
}
