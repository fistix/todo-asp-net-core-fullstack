using Fistix.Training.Domain.PayPalModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fistix.Training.Domain.Commands.Paypal
{
  public class CreateSubscriptionPlanCommandResult
  {
    public CreatePlanModel Payload { get; set; }
  }
}
