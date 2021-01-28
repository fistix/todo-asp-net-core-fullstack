using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Fistix.Training.Domain.Commands.Profiles
{
  public class UpdateMyProfilePictureCommand : IRequest<UpdateMyProfilePictureCommandResult>
  {
    [JsonIgnore]
    public string Email { get; set; }
    public IFormFile ProfilePicture { get; set; }
  }
}
