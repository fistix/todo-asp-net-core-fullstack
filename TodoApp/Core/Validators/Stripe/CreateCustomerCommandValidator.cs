using Fistix.Training.Domain.Commands.Customers;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fistix.Training.Core.Validators.Stripe
{
  public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
  {
    public CreateCustomerCommandValidator()
    {
      RuleFor(x => x.FirstName).Length(3, 10);
      RuleFor(x => x.LastName).NotEmpty();
      RuleFor(x => x.Email).EmailAddress();
    }
  }
}
