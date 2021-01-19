using Fistix.Training.Domain.Commands.Tasks;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fistix.Training.Core.Validators.Tasks
{
  public class UpdateTaskCommandValidator : AbstractValidator<UpdateTaskCommand>
  {
    public UpdateTaskCommandValidator()
    {
      //RuleFor(x => x.Id).NotEmpty();
      RuleFor(x => x.Title).Length(1, 30);
      RuleFor(x => x.Description).NotEmpty();
    }
  }
}
