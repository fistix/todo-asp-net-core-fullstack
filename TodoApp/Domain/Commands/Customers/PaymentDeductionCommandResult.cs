using Fistix.Training.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fistix.Training.Domain.Commands.Customers
{
  public class PaymentDeductionCommandResult
  {
    public CustomerDto Payload { get; set; }

  }
}
