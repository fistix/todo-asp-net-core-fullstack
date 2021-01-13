using Fistix.Training.Domain.Queries.Tasks;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fistix.Training.Core.Validators.Tasks
{
  public class GetTaskDetailByIdQueryValidator : AbstractValidator<GetTaskDetailByIdQuery>
  {
    public GetTaskDetailByIdQueryValidator()
    {
      RuleFor(x => x.Id).NotEmpty();
    }
  }
}
