using Fistix.Training.Domain.Commands.Paypal;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fistix.Training.Core.Validators.PayPal
{
  public class CreateSubscriptionPlanCommandValidator : AbstractValidator<CreateSubscriptionPlanCommand>
  {
    public CreateSubscriptionPlanCommandValidator()
    {
      //RuleFor(x=>x.)
    }
  }
}
