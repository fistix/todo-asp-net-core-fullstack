using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fistix.Training.Domain.Commands.Tasks
{
    public class DeleteTaskCommand : IRequest<DeleteTaskCommandResult>
    {
        public Guid Id { get; set; }
    }
}
