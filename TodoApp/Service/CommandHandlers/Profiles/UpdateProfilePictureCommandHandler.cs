using AutoMapper;
using Azure.Storage.Blobs;
using Fistix.Training.Core;
using Fistix.Training.Domain.Commands.Profiles;
using Fistix.Training.Service.AzureFileService;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fistix.Training.Service.CommandHandlers.Profiles
{
    public class UpdateProfilePictureCommandHandler : IRequestHandler<UpdateProfilePictureCommand, UpdateProfilePictureCommandResult>
    {
        private readonly IMapper _mapper = null;
        private readonly IProfileRepository _profileRepository = null;
        private readonly IFileService _fileService = null;
        private readonly IConfiguration _configuration = null;
        public UpdateProfilePictureCommandHandler(IMapper mapper, IProfileRepository profileRepository, IFileService fileService, IConfiguration configuration)
        {
            _mapper = mapper;
            _profileRepository = profileRepository;
            _fileService = fileService;
            _configuration = configuration;
        }
        public async Task<UpdateProfilePictureCommandResult> Handle(UpdateProfilePictureCommand command, CancellationToken cancellationToken)
        {
            var profile = await _profileRepository.GetById(command.Id);

            var fileUploadURI = await _fileService.UploadFileAsync(profile.ProfileId.ToString(), _configuration["AzureContainer"], command.ProfilePicture.OpenReadStream(), command.ProfilePicture.ContentType, command.ProfilePicture.FileName);

            profile.ProfilePictureUrl = fileUploadURI.ToString();
            var result = await _profileRepository.Update(profile);
            if (result != null)
            {
                return new UpdateProfilePictureCommandResult()
                {
                    UserProfileUrl = result.ProfilePictureUrl
                };
            }
            return null;
            //throw new NotImplementedException();
        }
    }
}
