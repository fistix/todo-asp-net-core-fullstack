using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Fistix.Training.Domain.Commands.Profiles
{
  public class CreateMyProfileCommand : IRequest<CreateMyProfileCommandResult>
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }

    [JsonIgnore]
    public string Email { get; set; }
  }
}
