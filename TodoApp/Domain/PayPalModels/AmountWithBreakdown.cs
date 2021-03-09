using System;
using System.Collections.Generic;
using System.Text;

namespace Fistix.Training.Domain.PayPalModels
{
  public class AmountWithBreakdown
  {
    public GrossAmount gross_amount { get; set; }
    public FeeAmount fee_amount { get; set; }
    public NetAmount net_amount { get; set; }
  }
}
