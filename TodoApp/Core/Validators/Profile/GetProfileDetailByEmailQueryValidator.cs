using Fistix.Training.Domain.Queries.Profiles;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fistix.Training.Core.Validators.Profile
{
  public class GetProfileDetailByEmailQueryValidator : AbstractValidator<GetProfileDetailByEmailQuery>
  {
    public GetProfileDetailByEmailQueryValidator()
    {
      RuleFor(x => x.Email).EmailAddress();
    }
  }
}
