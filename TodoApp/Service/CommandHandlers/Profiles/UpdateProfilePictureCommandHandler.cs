using AutoMapper;
using Azure.Storage.Blobs;
using Fistix.Training.Core;
using Fistix.Training.Core.Config;
using Fistix.Training.Domain.Commands.Profiles;
using Fistix.Training.Service.AzureFileService;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
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
    private readonly MasterConfig _masterConfig = null;
    public UpdateProfilePictureCommandHandler(IMapper mapper, IProfileRepository profileRepository, IFileService fileService, IConfiguration configuration, MasterConfig masterConfig)
    {
      _mapper = mapper;
      _profileRepository = profileRepository;
      _fileService = fileService;
      _configuration = configuration;
      _masterConfig = masterConfig;

    }
    public async Task<UpdateProfilePictureCommandResult> Handle(UpdateProfilePictureCommand command, CancellationToken cancellationToken)
    {
      var profile = await _profileRepository.GetById(command.Id);

      if (!String.IsNullOrEmpty(profile.ProfilePictureUrl))
      {
        //string deleteFileName = Path.GetFileName(profile.ProfilePictureUrl).Split("%2F")[1];
        string deleteFileName = Path.GetFileName(profile.ProfilePictureUrl);
        var response = await _fileService.DeleteFileAsync
            (/*profile.ProfileId.ToString(),*/ _masterConfig.AzureStorageConfig.AzureContainer
            /*_configuration["AzureContainer"]*/, deleteFileName);
      }
      //For generating file name
      var extension = Path.GetExtension(command.ProfilePicture.FileName);
      var fileName = Path.Combine($"{command.Id}{extension}");

      var fileUploadURI = await _fileService.UploadFileAsync(/*profile.ProfileId.ToString(),*/
        _masterConfig.AzureStorageConfig.AzureContainer
          /*_configuration["AzureContainer"]*/, command.ProfilePicture.OpenReadStream(),
          command.ProfilePicture.ContentType, /*command.ProfilePicture.FileName*/fileName);


      profile.ProfilePictureUrl = fileUploadURI.AbsoluteUri;
      var result = await _profileRepository.Update(profile);
      if (!result)
      {
        profile.ProfilePictureUrl = "";
      }

      return new UpdateProfilePictureCommandResult()
      {
        UserProfileUrl = profile.ProfilePictureUrl
      };

    }
  }
}
