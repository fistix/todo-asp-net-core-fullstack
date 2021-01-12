using MediatR;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fistix.Training.Domain.Commands.Profiles
{
  public class UpdateProfilePictureCommand : IRequest<UpdateProfilePictureCommandResult>
  {
    [JsonIgnore]
    public Guid Id { get; set; }
    public IFormFile ProfilePicture { get; set; }
  }
}
