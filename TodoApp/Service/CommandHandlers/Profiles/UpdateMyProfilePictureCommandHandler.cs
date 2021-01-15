﻿using AutoMapper;
using Fistix.Training.Core;
using Fistix.Training.Core.Config;
using Fistix.Training.Domain.Commands.MyProfile;
using Fistix.Training.Service.AzureFileService;
using MediatR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fistix.Training.Service.CommandHandlers.MyProfile
{
  public class UpdateMyProfilePictureCommandHandler : IRequestHandler<UpdateMyProfilePictureCommand, UpdateMyProfilePictureCommandResult>
  {
    private readonly IMapper _mapper = null;
    private readonly IFileService _fileService = null;
    private readonly MasterConfig _masterConfig = null;
    private readonly IProfileRepository _profileRepository = null;
    private readonly ICurrentUserService _currentUserService = null;
    public UpdateMyProfilePictureCommandHandler(IMapper mapper, IFileService fileService, 
      MasterConfig masterConfig, IProfileRepository profileRepository, ICurrentUserService currentUserService)
    {
      _mapper = mapper;
      _fileService = fileService;
      _masterConfig = masterConfig;
      _profileRepository = profileRepository;
      _currentUserService = currentUserService;

    }
    public async Task<UpdateMyProfilePictureCommandResult> Handle(UpdateMyProfilePictureCommand command, CancellationToken cancellationToken)
    {
      command.Email = _currentUserService.Email;
      //var profile = await _profileRepository.GetById(command.Id);
      var profile = await _profileRepository.GetByEmail(command.Email);

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
      var fileName = Path.Combine($"{profile.ProfileId}{extension}");

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

      return new UpdateMyProfilePictureCommandResult()
      {
        ProfilePictureUrl = profile.ProfilePictureUrl
      };

    }
  }
}
