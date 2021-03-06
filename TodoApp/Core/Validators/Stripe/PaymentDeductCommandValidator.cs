﻿using Fistix.Training.Domain.Commands.Stripe;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fistix.Training.Core.Validators.Stripe
{
  public class PaymentDeductCommandValidator : AbstractValidator<PaymentDeductCommand>
  {
    public PaymentDeductCommandValidator()
    {
      RuleFor(x => x.CustomerId).NotEmpty();
      RuleFor(x => x.StripeCustomerId).NotEmpty();
      RuleFor(x => x.Amount).GreaterThan(0);
    }
  }
}
