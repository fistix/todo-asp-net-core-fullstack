using Fistix.Training.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fistix.Training.Domain.Commands.Stripe
{
  public class PaymentDeductCommandResult
  {
    //public CustomerDto Payload { get; set; }
    public bool IsSucceed { get; set; }

  }
}
