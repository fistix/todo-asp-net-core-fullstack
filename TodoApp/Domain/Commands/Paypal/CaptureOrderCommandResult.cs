using Fistix.Training.Domain.PayPalModels;
using PayPalCheckoutSdk.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fistix.Training. Domain.Commands.Paypal
{
  public class CaptureOrderCommandResult
  {
    public Order Order { get; set; }
    //public OrderModel Order { get; set; }
  }
}
