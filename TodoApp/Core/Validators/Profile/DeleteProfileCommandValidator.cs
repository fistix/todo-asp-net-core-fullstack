using Fistix.Training.Domain.Commands.Profiles;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fistix.Training.Core.Validators.Profile
{
  public class DeleteProfileCommandValidator : AbstractValidator<DeleteProfileCommand>
  {
    public DeleteProfileCommandValidator()
    {
      RuleFor(x => x.Id).NotEmpty();
    }
  }
}
