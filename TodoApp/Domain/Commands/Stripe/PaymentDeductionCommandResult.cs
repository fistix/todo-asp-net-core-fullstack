using System;
using System.Collections.Generic;
using System.Text;

namespace Fistix.Training.Domain.Commands.Stripe
{
  public class PaymentDeductionCommandResult
  {
    public string PaymentIntentId { get; set; }
  }
}
