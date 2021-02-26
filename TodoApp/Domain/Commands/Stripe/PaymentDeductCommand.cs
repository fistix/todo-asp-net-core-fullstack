using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fistix.Training.Domain.Commands.Stripe
{
  public class PaymentDeductCommand : IRequest<PaymentDeductCommandResult>
  {
    //public string Email { get; set; }
    public string StripeCustomerId { get; set; }
    public string CustomerId { get; set; }
    public int Amount { get; set; }
  }
}
