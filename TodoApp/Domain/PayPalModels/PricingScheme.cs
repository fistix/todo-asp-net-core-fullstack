using System;
using System.Collections.Generic;
using System.Text;

namespace Fistix.Training.Domain.PayPalModels
{
  public class PricingScheme
  {
    public int version { get; set; }
    public FixedPrice fixed_price { get; set; }
    public DateTime create_time { get; set; }
    public DateTime update_time { get; set; }
  }
}
