using System;
using System.Collections.Generic;
using System.Text;

namespace Fistix.Training.Domain.PayPalModels
{
  public class SetupFee
  {
    public string currency_code { get; set; }
    public string value { get; set; }
  }
}
