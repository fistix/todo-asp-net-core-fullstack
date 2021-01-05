using Fistix.Training.Domain.Commands.Tasks;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fistix.Training.Core.Validators.Tasks
{
    public class AttachUserWithTaskCommandValidator :AbstractValidator<AttachUserWithTaskCommand>
    {
        public AttachUserWithTaskCommandValidator()
        {
            RuleFor(x => x.TaskId).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
