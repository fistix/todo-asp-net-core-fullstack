using AutoMapper;
using Fistix.Training.Core;
using Fistix.Training.Domain.Commands.Profiles;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Fistix.Training.Service.CommandHandlers.Profiles
{
  public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, UpdateProfileCommandResult>
  {
    private readonly IMapper _mapper = null;
    private readonly IProfileRepository _profileRepository = null;
    public UpdateProfileCommandHandler(IMapper mapper, IProfileRepository profileRepository)
    {
      _mapper = mapper;
      _profileRepository = profileRepository;
    }
    public async Task<UpdateProfileCommandResult> Handle(UpdateProfileCommand command, CancellationToken cancellationToken)
    {
      var profile = await _profileRepository.GetById(command.Id);
      var updatedProfile = _mapper.Map(command, profile);
      var response = await _profileRepository.Update(updatedProfile);
      if (response)
      {
        return new UpdateProfileCommandResult()
        {
          Payload = _mapper.Map<Domain.Dtos.ProfileDto>(updatedProfile)
        };
      }
      else
      {
        return new UpdateProfileCommandResult()
        {
          Payload = _mapper.Map<Domain.Dtos.ProfileDto>(profile)
        };
      }
    }
  }
}
