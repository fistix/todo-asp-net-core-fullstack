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
        private readonly IProfileRepository _profileRepository = null;
        private readonly IMapper _mapper = null;
        public CreateProfileCommandHandler(IProfileRepository profileRepository,IMapper mapper)
        {
            _profileRepository = profileRepository;
            _mapper = mapper;
        }
        public async Task<CreateProfileCommandResult> Handle(CreateProfileCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var profile = _mapper.Map<Domain.DataModels.Profile>(command);
                profile.Id = Guid.NewGuid();
                var response = await _profileRepository.Create(profile);
                return new CreateProfileCommandResult()
                {
                    Payload = _mapper.Map<ProfileDto>(response)
                };
            }
            catch (ArgumentException ex)
            {
                throw;
            }
           
            
            //throw new NotImplementedException();
        }
    }
}
