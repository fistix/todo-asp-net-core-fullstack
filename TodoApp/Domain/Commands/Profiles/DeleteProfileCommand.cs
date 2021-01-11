using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fistix.Training.Domain.Commands.Profiles
{
    public class DeleteProfileCommand : IRequest<DeleteProfileCommandResult>
    {
        public Guid Id { get; set; }
    }
}
