using System;
using System.Collections.Generic;
using System.Text;

namespace Fistix.Training.Domain.PayPalModels
{

  public class BillingCycle
  {
    public PricingScheme pricing_scheme { get; set; }
    public Frequency frequency { get; set; }
    public string tenure_type { get; set; }
    public int sequence { get; set; }
    public int total_cycles { get; set; }
  }
}
