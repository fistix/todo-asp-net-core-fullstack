using Fistix.Training.Domain.Queries.Profiles;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fistix.Training.Core.Validators.Profile
{
    public class GetProfileDetailByIdQueryValidator : AbstractValidator<GetProfileDetailByIdQuery>
    {
        public GetProfileDetailByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
