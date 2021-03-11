using Fistix.Training.Domain.PayPalModels;
using PayPalCheckoutSdk.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fistix.Training.Domain.Commands.Paypal
{
  public class CreateOrderCommandResult
  {
    //public Order Payload { get; set; }
    public OrderModel Payload { get; set; }
  }
}
