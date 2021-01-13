using Fistix.Training.Domain.Commands.Tasks;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fistix.Training.Core.Validators.Tasks
{
  public class DeleteTaskCommandValidator : AbstractValidator<DeleteTaskCommand>
  {
    public DeleteTaskCommandValidator()
    {
      RuleFor(x => x.Id).NotEmpty();
    }
  }
}
