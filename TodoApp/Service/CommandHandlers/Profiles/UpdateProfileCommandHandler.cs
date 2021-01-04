﻿using AutoMapper;
using Fistix.Training.Core;
using Fistix.Training.Domain.Commands.Profiles;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fistix.Training.Service.CommandHandlers.Profiles
{
    public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, UpdateProfileCommandResult>
    {
        private readonly IMapper _mapper = null;
        private readonly IProfileRepository _profileRepository = null;
        public UpdateProfileCommandHandler(IMapper mapper,IProfileRepository profileRepository)
        {
            _mapper = mapper;
            _profileRepository = profileRepository;
        }
        public async Task<UpdateProfileCommandResult> Handle(UpdateProfileCommand command, CancellationToken cancellationToken)
        {
            var profile = _mapper.Map<Domain.DataModels.Profile>(command);
            var result = await _profileRepository.Update(profile);
            if (result != null)
            {
                return new UpdateProfileCommandResult()
                {
                    Payload = _mapper.Map<Domain.Dtos.ProfileDto>(result)
                };
            }
            return null;
            
            //throw new NotImplementedException();
        }
    }
}
