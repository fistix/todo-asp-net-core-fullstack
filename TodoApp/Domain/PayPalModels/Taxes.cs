using System;
using System.Collections.Generic;
using System.Text;

namespace Fistix.Training.Domain.PayPalModels
{
  public class Taxes
  {
    public string percentage { get; set; }
    public bool inclusive { get; set; }
  }
}
