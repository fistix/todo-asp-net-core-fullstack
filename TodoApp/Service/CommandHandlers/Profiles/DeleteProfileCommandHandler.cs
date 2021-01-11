using AutoMapper;
using Fistix.Training.Core;
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
        private readonly IMapper _mapper = null;
        private readonly IProfileRepository _profileRepository = null;
        private readonly IFileService _fileService = null;
        private readonly IConfiguration _configuration = null;
        public DeleteProfileCommandHandler(IMapper mapper, IProfileRepository profileRepository, IFileService fileService, IConfiguration configuration)
        {
            _mapper = mapper;
            _profileRepository = profileRepository;
            _fileService = fileService;
            _configuration = configuration;
        }
        public async Task<DeleteProfileCommandResult> Handle(DeleteProfileCommand command, CancellationToken cancellationToken)
        {
            var result = await _profileRepository.GetById(command.Id);
            bool res = false;
            if (result != null)
            {
                string fileName = Path.GetFileName(result.ProfilePictureUrl).Split("%2F")[1];
                await _fileService.DeleteFileAsync(
                    result.ProfileId.ToString(),
                    _configuration["AzureContainer"], 
                    fileName);
                res = await _profileRepository.Delete(command.Id);
            }

            return new DeleteProfileCommandResult() 
            { 
                IsSucceed = res
            };
            //throw new NotImplementedException();
        }
    }
}
