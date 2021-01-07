using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fistix.Training.Domain.Commands.Tasks
{
    public class UnAttachUserWithTaskCommand : IRequest<UnAttachUserWithTaskCommandResult>
    {
        public Guid TaskId { get; set; }
        public Guid UserId { get; set; }
    }
}
