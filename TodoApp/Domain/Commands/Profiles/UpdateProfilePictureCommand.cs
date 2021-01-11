using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fistix.Training.Domain.Commands.Profiles
{
    public class UpdateProfilePictureCommand : IRequest<UpdateProfilePictureCommandResult>
    {
        public Guid Id { get; set; }
        public IFormFile ProfilePicture { get; set; }
    }
}
