using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Fistix.Training.Domain.Commands.Tasks
{
  public class UpdateMyTaskCommand : IRequest<UpdateMyTaskCommandResult>
  {
    [JsonIgnore]
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool Active { get; set; }
  }
}
