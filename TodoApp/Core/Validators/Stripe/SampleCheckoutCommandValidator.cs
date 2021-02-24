using Fistix.Training.Domain.Commands.Customers;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fistix.Training.Core.Validators.Stripe
{
  public class SampleCheckoutCommandValidator : AbstractValidator<SampleCheckoutCommand>
  {
    public SampleCheckoutCommandValidator()
    {
      RuleFor(x => x.Amount).GreaterThan(0);
      RuleFor(x => x.ProductName).NotEmpty();
      RuleFor(x => x.Email).EmailAddress();
    }
  }
}
