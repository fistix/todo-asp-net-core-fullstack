using Fistix.Training.Domain.PayPalModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fistix.Training.Domain.Commands.Paypal
{
  public class AttachSubscriptionIdToCustomerCommandResult
  {
    public SubscriptionDetailByIdResponseModel Payload { get; set; }
    public bool IsSucceed { get; set; }
  }
}
