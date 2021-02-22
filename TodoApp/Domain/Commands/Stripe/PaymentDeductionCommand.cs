﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fistix.Training.Domain.Commands.Stripe
{
  public class PaymentDeductionCommand : IRequest<PaymentDeductionCommandResult>
  {
    //public string Email { get; set; }
    public string CustomerId { get; set; }
    public long Amount { get; set; }
  }
}