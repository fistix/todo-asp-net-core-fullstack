using Fistix.Training.Domain.Queries.Customers;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fistix.Training.Core.Validators.Customers
{
  public class GetCustomerDetailByEmailQueryValidator : AbstractValidator<GetCustomerDetailByEmailQuery>
  {
    public GetCustomerDetailByEmailQueryValidator()
    {
      RuleFor(x => x.Email).EmailAddress();
    }
  }
}
