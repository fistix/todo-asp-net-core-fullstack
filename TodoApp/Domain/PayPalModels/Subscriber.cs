using System;
using System.Collections.Generic;
using System.Text;

namespace Fistix.Training.Domain.PayPalModels
{
  public class Subscriber
  {
    public Name name { get; set; }
    public string email_address { get; set; }
    public ShippingAddress shipping_address { get; set; }
  }
}
