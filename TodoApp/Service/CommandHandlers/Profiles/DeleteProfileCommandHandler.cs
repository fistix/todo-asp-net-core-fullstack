using AutoMapper;
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
  public class DeleteProfileCommandHandler : IRequestHandler<DeleteProfileCommand, DeleteProfileCommandResult>
  {
    private readonly IFileService _fileService = null;
    private readonly MasterConfig _masterConfig = null;
    private readonly IProfileRepository _profileRepository = null;
    public DeleteProfileCommandHandler(IFileService fileService, MasterConfig masterConfig, IProfileRepository profileRepository)
    {
      _fileService = fileService;
      _masterConfig = masterConfig;
      _profileRepository = profileRepository;
    }
    public async Task<DeleteProfileCommandResult> Handle(DeleteProfileCommand command, CancellationToken cancellationToken)
    {
      var profile = await _profileRepository.GetById(command.Id);
      bool response = false;

      if (!String.IsNullOrEmpty(profile.ProfilePictureUrl))
      {
        string fileName = Path.GetFileName(profile.ProfilePictureUrl);
        await _fileService.DeleteFileAsync(_masterConfig.AzureStorageConfig.AzureContainer, fileName);
      }

      response = await _profileRepository.Delete(command.Id);

      return new DeleteProfileCommandResult()
      {
        IsSucceed = response
      };
    }
  }
}
