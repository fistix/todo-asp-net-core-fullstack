using System;
using System.Collections.Generic;
using System.Text;

namespace Fistix.Training.Domain.PayPalModels
{
  public class LastPayment
  {
    public Amount amount { get; set; }
    public DateTime time { get; set; }
  }
}
